using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SReplace
{
	class Program
	{
		static void Main(string[] args)
		{
			Parameter parameter = null;
			try
			{
				parameter = new Parameter(args);


				Encoding enc = null;

				if (parameter.ReadEncoding != null && parameter.IsEncoding == true)
				{
					throw new SystemException("エンコーディングの設定と独自のエンコーディングアルゴリズムの使用は同時に設定できません。パラメータ/Cと/P");
				}

				if (parameter.ReadEncoding != null)
				{
					enc = parameter.ReadEncoding;
				}
				if (parameter.IsEncoding == true)
				{
					//独自のEncodingアルゴリズム
					enc = Facade.File.GetEncoding(parameter.FromFile);
				}
				
				string text = null;
				if (enc == null)
				{
					text = File.ReadAllText(parameter.FromFile);
				}
				else
				{
					text = File.ReadAllText(parameter.FromFile, enc);
				}



				//指定文字による置換を実施
				if (parameter.OldChar != null && parameter.IsRegex == false)
				{
					text = text.Replace(parameter.OldChar, parameter.NewChar);
				}

				//正規表現による文字列の置換を実施
				if (parameter.OldChar != null && parameter.IsRegex == true)
				{
					Regex reg = new Regex(parameter.OldChar);

					text = reg.Replace(text,parameter.NewChar);
				}


				//環境変数による置換を実施
				if (parameter.IsEnvironment == true)
				{
					IEnumerator  e = System.Environment.GetEnvironmentVariables().GetEnumerator();
					while (e.MoveNext())
					{
						DictionaryEntry dic = (DictionaryEntry)e.Current;
                        if (dic.Value.ToString().ToUpper() == "StringEmpty".ToUpper())
                        {
                            text = text.Replace("%" + dic.Key.ToString() + "%", "");
                        }
                        else
                        {
							//Console.WriteLine($"key={dic.Key.ToString()}, value={dic.Value.ToString()}");
                            text = text.Replace("%" + dic.Key.ToString() + "%", dic.Value.ToString());
                        }
					}
				}

				bool replaceFlg = false;
				if (File.Exists(parameter.ToFile) == true)
				{
					replaceFlg = true;
				}

				//空行を削除
				if (parameter.IsDeleteLine == true)
				{
					StringBuilder sb = new StringBuilder(1024);
					//空行を削除
					string[] lines = text.Split(new char[] { '\n','\r'});

					foreach (string line in lines)
					{
						if (line.TrimEnd().Length == 0) continue;
						sb.AppendLine(line);
					}
					text = sb.ToString();
				}

				
				if (parameter.IsReadOnly == true)
				{
					FileInfo fi = new FileInfo(parameter.ToFile);
					if (fi.Exists == true && fi.IsReadOnly == true)
					{
						fi.IsReadOnly = false;
					}
				}

				if (enc == null)
				{
					File.WriteAllText(parameter.ToFile, text);
				}
				else
				{
					File.WriteAllText(parameter.ToFile, text, enc);
				}

				if (parameter.IsTimeCopy == true)
				{
					FileInfo fromFile = new FileInfo(parameter.FromFile);
					FileInfo toFile = new FileInfo(parameter.ToFile);
					toFile.LastAccessTime = fromFile.LastAccessTime;
					toFile.CreationTime = fromFile.CreationTime;
					toFile.LastWriteTime = fromFile.LastWriteTime;

				}

				string codePage = "";
				//string codePage = "["  + enc.WebName + "]";
				if (replaceFlg == true)
				{
					Console.Out.WriteLine(parameter.ToFile + "を置換しました。" + codePage);
				}
				else
				{
					Console.Out.WriteLine(parameter.ToFile + "を作成しました。" + codePage);
				}

			}
			catch (Exception ex)
			{
				if (parameter != null)
				{
					WriteException(ex, parameter.IsDebug);
				}
				else
				{
					WriteException(ex, false);
				}
			}

			//Console.In.ReadLine();
		}

		private static void WriteException(Exception ex, bool isDebug)
		{
			if (isDebug == false)
			{
				Console.Out.WriteLine(Dump.GetExceptionMessageForConsumer(ex));
			}
			else
			{
				Console.Out.WriteLine("---<例外内容、コンシュマー向け>---");
				Console.Out.WriteLine(Dump.GetExceptionMessageForConsumer(ex));

				Console.Out.WriteLine("---<例外の内容、開発者向け>---");
				Console.Out.WriteLine(Dump.GetExceptionMessageForDeveloper(ex));
			}
		}
	}
}
