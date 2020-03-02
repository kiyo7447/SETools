using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	/// <summary>
	/// パラメータの基底クラス
	/// </summary>
	public abstract class AbstractParameter : IProperty
	{
		List<string> _argumentsExceptOptions = new List<string>();
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="args">プログラムの起動パラメータ</param>
		public AbstractParameter(string[] args)
		{
			foreach(string arg in args)
			{
				_argumentsExceptOptions.Add(arg);
			}
		}


		/// <summary>
		/// 
		/// getDataCount		0		=		現在のデータを全て返す
		/// getDataCount		n		=		現在のデータ数かgetDataCountの小さいほうを返す
		/// 
		/// requireDataCount	0		=		パラメータの数のチェックを行わない
		/// requireDataCount	n		=		パラメータの数のチェック
		/// </summary>
		/// <param name="getDataCount">0:無制限</param>
		/// <param name="requireDataCount">0:パラメータなしを許可</param>
		/// <returns></returns>
		protected string[] GetParameter(int getDataCount, int requireDataCount)
		{
			//必須入力チェック
			if (requireDataCount != 0 && _argumentsExceptOptions.Count < requireDataCount)
			{
				throw new ApplicationException("必須パラメータの数が足りていません。必要な必須パラメータ数=" + requireDataCount.ToString() + "、必須指定されたパラメータの数=" + _argumentsExceptOptions.Count + "、パラメータの内容=" + Dump.GetMessage(_argumentsExceptOptions) + "、詳細はヘルプを参照してください。");
			}

			int dataCount;
			if (getDataCount == 0)
			{
				//全件を返す
				dataCount = _argumentsExceptOptions.Count;
			}
			else
			{
				//最大値を求めている件数でデータを返す
				if (_argumentsExceptOptions.Count > getDataCount)
				{
					dataCount = getDataCount;
				}
				else
				{
					dataCount = _argumentsExceptOptions.Count;
				}
			}

			string[] ret = new string[dataCount];
			for (int idxRet = 0; idxRet < dataCount; idxRet++)
			{
				ret[idxRet] = _argumentsExceptOptions[0];
				_argumentsExceptOptions.RemoveAt(0);
			}
			return ret;
		}


		/// <summary>
		/// パラメータを取得します。
		/// </summary>
		/// <param name="key"></param>
		/// <param name="getMaxDataCount">０：最大数は決まっていません。</param>
		/// <param name="requireDataCount">必要な最低限のパラメータ数</param>
		/// <returns></returns>
		protected string[] GetParameter(string key, int getMaxDataCount, int requireDataCount)
		{
			int idx;
			List<string> retO = new List<string>();
			for(idx = 0; idx < _argumentsExceptOptions.Count; idx++) 
			{
				bool existsFlg = false;
				if (_argumentsExceptOptions[idx].ToUpper() == "/" + key.ToUpper())
				{
					existsFlg = true;
				}
				else if (_argumentsExceptOptions[idx].ToUpper() == "-" + key.ToUpper())
				{
					existsFlg = true;
				}

				if (existsFlg == true)
				{
					break;
				}
			}
			if (idx == _argumentsExceptOptions.Count)
			{
				//パラメータなし
				if (requireDataCount > 0)
				{
					throw new SystemException(string.Format("パラメータ/{0}は、必要のパラメータ数が{1}個必要ですが、一つも見つかりませんでした。", new object[] { key, requireDataCount}));
				}
				return null;
			}
			else
			{
				//パラメータあり（キーを削除）
				_argumentsExceptOptions.RemoveAt(idx);

				int maxDataCount = getMaxDataCount==0?int.MaxValue:getMaxDataCount;
				for (int idxRet = 0; idxRet < maxDataCount; idxRet++)
				{
					//解析するべきキーがない場合は解析の終了
					if (idx >= _argumentsExceptOptions.Count)
					{
						break;
					}

					//次のキーがある場合は解析を終了
					if (_argumentsExceptOptions[idx].Substring(0, 1) == "-" || _argumentsExceptOptions[idx].Substring(0, 1) == "/")
					{
						break;
					}


					retO.Add(_argumentsExceptOptions[idx]);

					//処理後は削除
					_argumentsExceptOptions.RemoveAt(idx);
				}

				//必須分のキーがあったかのかをチェック
				if (requireDataCount > 0 && retO.Count < requireDataCount)
				{
					throw new SystemException(string.Format("パラメータ/{0}に続く引数の数が足りていません。必要なパラメータの数は、{1}個ですが、{2}個しか見つかりませんでした。", new object[]{key, requireDataCount, retO.Count}));
				}

				string[] ret = new string[retO.Count];
				for(int i = 0;i < retO.Count;i++) {
					ret[i] = retO[i];
				}
				return ret;
			}

		}

		/// <summary>
		/// 排他キーの取得を行います。
		/// </summary>
		/// <param name="key"></param>
		/// <param name="dataCount"></param>
		/// <returns>第一パラメータ=キー、第二パラメータ以降はデータ</returns>
		protected string[] GetParameterExclusive(string[] key, int dataCount)
		{
			return null;
		}

		/// <summary>
		/// パラメータの解析処理
		/// </summary>
		/// <param name="key">取得するパラメータのキー</param>
		/// <param name="dataCount">取得するパラメータの数</param>
		/// <returns></returns>
		protected string[] GetParameter(string key, int dataCount)
		{
			int idx;
			for(idx = 0; idx < _argumentsExceptOptions.Count; idx++) 
			{
				bool existsFlg = false;
				if (_argumentsExceptOptions[idx].ToUpper() == "/" + key.ToUpper())
				{
					existsFlg = true;
				}
				else if (_argumentsExceptOptions[idx].ToUpper() == "-" + key.ToUpper())
				{
					existsFlg = true;
				}

				if (existsFlg == true)
				{
					break;
				}
			}
			if (idx == _argumentsExceptOptions.Count)
			{
				//パラメータなし
				return null;
			}
			else
			{
				//パラメータあり
				_argumentsExceptOptions.RemoveAt(idx);

				string[] ret = new string[dataCount];
				for (int idxRet = 0; idxRet < dataCount; idxRet++)
				{
					if (idx >= _argumentsExceptOptions.Count)
					{
						throw new ApplicationException("パラメータ/" + key + "に続く引数がありません。引数を正しく指定してください。");
					}
					ret[idxRet] = _argumentsExceptOptions[idx];
					_argumentsExceptOptions.RemoveAt(idx);
				}

				return ret;
			}
		}

		//public List<string> ArgumentsExceptOptions
		//{
		//    get
		//    {
		//        return _argumentsExceptOptions;
		//    }
		//}

		protected Encoding GetEncoding(string encodingString)
		{
			return ALib.AEncoding.GetEncodingString(encodingString);
		}
	}
}
