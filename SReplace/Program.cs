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
					throw new SystemException("�G���R�[�f�B���O�̐ݒ�ƓƎ��̃G���R�[�f�B���O�A���S���Y���̎g�p�͓����ɐݒ�ł��܂���B�p�����[�^/C��/P");
				}

				if (parameter.ReadEncoding != null)
				{
					enc = parameter.ReadEncoding;
				}
				if (parameter.IsEncoding == true)
				{
					//�Ǝ���Encoding�A���S���Y��
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



				//�w�蕶���ɂ��u�������{
				if (parameter.OldChar != null && parameter.IsRegex == false)
				{
					text = text.Replace(parameter.OldChar, parameter.NewChar);
				}

				//���K�\���ɂ�镶����̒u�������{
				if (parameter.OldChar != null && parameter.IsRegex == true)
				{
					Regex reg = new Regex(parameter.OldChar);

					text = reg.Replace(text,parameter.NewChar);
				}


				//���ϐ��ɂ��u�������{
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

				//��s���폜
				if (parameter.IsDeleteLine == true)
				{
					StringBuilder sb = new StringBuilder(1024);
					//��s���폜
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
					Console.Out.WriteLine(parameter.ToFile + "��u�����܂����B" + codePage);
				}
				else
				{
					Console.Out.WriteLine(parameter.ToFile + "���쐬���܂����B" + codePage);
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
				Console.Out.WriteLine("---<��O���e�A�R���V���}�[����>---");
				Console.Out.WriteLine(Dump.GetExceptionMessageForConsumer(ex));

				Console.Out.WriteLine("---<��O�̓��e�A�J���Ҍ���>---");
				Console.Out.WriteLine(Dump.GetExceptionMessageForDeveloper(ex));
			}
		}
	}
}
