using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace ALib
{
	internal static class AEncoding
	{
		/// <summary>
		/// 文字列をEncodingオブジェクトに変換する失敗した場合は、
		/// </summary>
		/// <param name="encString">数字か文字を指定</param>
		/// <returns>Encodingオブジェクト</returns>
		internal static Encoding GetEncodingString(string encString)
		{
			Encoding encoding = null;
			try
			{
				int codePage;
				if (int.TryParse(encString, out codePage) == true)
				{
					encoding = Encoding.GetEncoding(codePage);
				}
				else
				{
					encoding = Encoding.GetEncoding(encString);
				}
			}
			catch (Exception ex)
			{
				throw new SystemException("指定されているコードページが不正です。コードページ=" + encString, ex);
			}

			return encoding;
		}


		/// <summary>
		/// 対象ファイルのエンコーディングを取得
		/// 作成中です。暫定ロジックのみ実装
		/// 
		/// 
		/// 今は制度が悪い、↓を参考に作り変えたほうが無難
		/// http://dobon.net/vb/dotnet/string/detectcode.html
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static Encoding GetEncodingFile(string fileName)
		{
			//TODO: UTF-8Nのみ対応
			//正しいロジックは、
			//http://kasumi.sakura.ne.jp/~gm/gpj/dev/tips/other/kanji.shtml

			//後に拡張し、テキストファイルを解析、出てくる文字を
			//分析し、統計的に判断を入れました。

			byte[] bytes = File.ReadAllBytes(fileName);

			//UTF-8Nか？（つまりBOMか否かで判断）先頭が、efbbbf
			if (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
			{
				return Encoding.UTF8;
			}
			//BOM付きUTF-16リトルエンディアン
			else if (bytes[0] == 0xFF && bytes[1] == 0xFE)
			{
				return Encoding.Unicode;
			}
			//BOM付きUTF-16ビッグエンディアン
			else if (bytes[0] == 0xFE && bytes[1] == 0xFF)
			{
				return Encoding.BigEndianUnicode;
			}
			else
			{
				//BOMが無い場合は、マニアックに調べる。統計的に99%は解析可能だと思う。
				return _DetectEncoding(bytes);
			}
		}

		private static Encoding _DetectEncoding(string fileName)
		{
			byte[] byt = File.ReadAllBytes(fileName);

			//return _DetectEncoding(b);


			FileStream stm = new FileStream(fileName, FileMode.Open);

			byte[] b = new byte[stm.Length];

			stm.Read(b, 0, (int)stm.Length);

			stm.Close();

			return _DetectEncoding(b);
		}


		/// <summary>
		/// 欠点があり、BOMなしのUTF-16やUTF-16 Big-Endianが対応し切れていない。
		///		秀丸ようにユーザに選ばせるべき。
		/// </summary>
		/// <param name="innerBytes"></param>
		/// <returns></returns>
		private static Encoding _DetectEncoding(byte[] innerBytes)
		{
			Encoding retEncode;

			// 文字コードを自動判定する
			// 該当文字コードではありえないことを示すフラグ群
			bool isNotUTF8 = false;
			bool isNotSJIS = false;
			bool isNotEUC = false;
			// 前の文字を保存するスタック
			Stack UTF8Stack = new Stack();
			Stack JISStack = new Stack();
			Stack SJISStack = new Stack();
			Stack EUCStack = new Stack();
			// その文字コードと見なしたときの文字の数
			int UTF8Count = 0;
			int SJISCount = 0;
			int EUCCount = 0;
			// 全バイトをループ処理（-2するのはバイト列に終端の「\0」が含まれるため）
			for (int cnt = 0; cnt <= innerBytes.Length - 2; cnt++)
			{
				// 1バイト読む
				byte c1 = innerBytes[cnt];
				// &H00ならUnicode確定
				if (c1 == 0)
				{
					retEncode = Encoding.Unicode;
					return retEncode;
				}
				// JISエスケープシーケンスの登場を調べる
				switch (c1)
				{
					case 27:
						// エスケープシーケンスのはじまり
						JISStack.Push(c1);
						break;
					case 36:
					case 40:
						// 「$」または「(」
						if (JISStack.Count != 0)
						{
							// 前のバイトが&H1B（ESC）かどうか
							if (((byte)JISStack.Peek()) == 27)
							{
								// このバイトを保存
								JISStack.Push(c1);
							}
						}
						break;
					case 64:
					case 66:
					case 74:
						// 「B」「J」「@」
						if (JISStack.Count == 2)
						{
							// 前の2バイトを取得
							byte c2 = (byte)JISStack.Pop();
							byte c3 = (byte)JISStack.Pop();
							// 前がエスケープシーケンスかどうか
							if (((c2 == 36) | (c2 == 40)) & (c3 == 27))
							{
								// エスケープが出てきたら、JISとして確定
								retEncode = Encoding.GetEncoding("ISO-2022-JP");
								return retEncode;
							}
						}

						break;
				}
				// シフトJISかどうか、そして、想定される文字数を調べる
				if ((SJISStack.Count == 0))
				{
					// 1バイト目
					if (((c1 >= 129) & (c1 <= 159)) | ((c1 >= 224) & (c1 <= 252)))
					{
						// 漢字1バイト目
						SJISStack.Push(c1);
					}
				}
				else if ((SJISStack.Count == 1))
				{
					// 2バイト目の判定
					if (((c1 >= 64) & (c1 <= 126)) | ((c1 >= 128) & (c1 <= 252)))
					{
						byte c2 = (byte)SJISStack.Pop();
						// 前バイトが有効なシフトJISの1バイト目
						SJISCount += 1;
					}
					else
					{
						// シフトJISではない
						isNotSJIS = true;
					}
				}
				// EUCかどうか、そして、想定される文字数を調べる
				if (c1 == 142)
				{
					// 半角カナのマークである
					EUCStack.Push(c1);
				}
				if (((c1 >= 161) & (c1 <= 254)))
				{
					// 漢字である
					if ((EUCStack.Count != 0))
					{
						// 先行バイトあり
						byte c2 = (byte)EUCStack.Pop();
						if ((c2 != 142))
						{
							// 半角カナではない
							EUCCount += 1;
						}
					}
					else
					{
						// 1バイト目
						EUCStack.Push(c1);
					}
				}

				// UTF-8かどうか、そして、想定される文字数を調べる
				if ((c1 >= 192 && c1 <= 223) || (c1 >= 224 && c1 <= 239))
				{
					// UTF-8の2バイトコードまたは3バイトコードの1バイト目
					// このバイトを保存
					UTF8Stack.Push(c1);
				}
				else if (c1 >= 128 && c1 <= 191)
				{
					// UTF-8の2バイト目または3バイト目
					if (UTF8Stack.Count == 1)
					{
						// 前1バイトを取得
						byte c2 = (byte)UTF8Stack.Pop();
						if ((c2 >= 192) & (c2 <= 223))
						{
							// UTF-8として正しい
							UTF8Count += 1;
						}
						else if ((c2 >= 224) & (c2 <= 239))
						{
							// もう1バイトある
							UTF8Stack.Push(c2);
							UTF8Stack.Push(c1);
						}
						else
						{
							// UTF-8ではない
							isNotUTF8 = true;
						}
					}
					else if (UTF8Stack.Count == 2)
					{
						// 前2バイトを取得
						byte c2 = (byte)UTF8Stack.Pop();
						byte c3 = (byte)UTF8Stack.Pop();
						// 2バイト目が&H80～&HBFで
						// 1バイト目が&HE0～&HEFならUTF8として正しい
						if ((c2 >= 128) & (c2 <= 191) & (c3 >= 224) & (c3 <= 239))
						{
							UTF8Count += 1;
						}
						else
						{
							// UTF8ではない
							isNotUTF8 = true;
						}
					}
				}
			}
			// 以下、統計情報をもとに、思わしき文字コードを判定
			// UnicodeやJISの場合には、上記ループ内で確定しているので、この処理には来ない
			// 残るはUTF-8、シフトJIS、EUC
			// フラグと登場頻度で決める
			if (isNotUTF8) UTF8Count = 0;
			if (isNotSJIS) SJISCount = 0;
			if (isNotEUC) EUCCount = 0;
			if (UTF8Count > Math.Max(SJISCount, EUCCount))
			{
				retEncode = Encoding.UTF8;
				return retEncode;
			}
			if (SJISCount > Math.Max(UTF8Count, EUCCount))
			{
				retEncode = Encoding.GetEncoding("Shift_JIS");
				return retEncode;
			}
			if (EUCCount > Math.Max(UTF8Count, SJISCount))
			{
				retEncode = Encoding.GetEncoding("EUC-JP");
				return retEncode;
			}
			// 確定できなければUTF-8にする
			retEncode = Encoding.UTF8;
			return retEncode;
		}

	}
}
