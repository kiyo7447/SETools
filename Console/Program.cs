using System;
using System.Diagnostics;
using System.Reflection;

namespace SConsole
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("�p�����[�^��������܂���B�}�j���A�����m�F���Ă��������B");
				return 1;
			}

			if (args[0].ToUpper() == "SHOW")
			{
				ShowCommand show = new ShowCommand(Console.Out);
				if (args.Length == 1)
				{
					return show.ShowAll();
				}
				else
				{
					return show.Show(args[1]);
				}
			}
			if (args[0].ToUpper() == "CLEAR")
			{
				Console.Clear();
				return 0;
			}
			/*
			 *����͂�����߂܂��B���R�̓v���Z�X�Ԃ̒l�̕ێ������ϐ��ł͂ł��Ȃ����߂ł��B 
			if (args[0].ToUpper() == "SW")
			{
				if (args[1].ToUpper() == "START")
				{
					Environment.SetEnvironmentVariable("SW", DateTime.Now.ToString(), EnvironmentVariableTarget.User);
					return 0;
				}
				else if (args[1].ToUpper() == "STOP")
				{
					string v = Environment.GetEnvironmentVariable("SW", EnvironmentVariableTarget.User);
					Console.Out.WriteLine(v);
					return 0;
				}

			}
			 */
			if (args[0].ToUpper() == "BEEP")
			{
				BeepCommand beep = new BeepCommand();
				if (args.Length == 1)
				{
					Console.Beep();
				}
				else
				{
					beep.Beep(args[1]);
				}
				return 0;
			}

			if (args[0].ToUpper() == "SET")
			{
				string[] kv = args[1].Split(new char[] { '=' });

				Type con = typeof(Console);
				PropertyInfo propertyInfo = con.GetProperty(kv[0]);

				if (propertyInfo == null)
				{
					Console.Out.WriteLine("�w�肳�ꂽ�L�[���͑��݂��܂���BKey=" + kv[0]);
					return 1;
				}

				Debug.WriteLine(propertyInfo.Name);
				object o = null;
				if (propertyInfo.PropertyType.FullName == "System.Int32")
				{
					o = Convert.ToInt32(kv[1]);
				}
				else if (propertyInfo.PropertyType.FullName == "System.Boolean")
				{
					o = Convert.ToBoolean(kv[1]);
				}
				else if (propertyInfo.PropertyType.FullName == "System.ConsoleColor")
				{
					ConsoleColor color = ConsoleColor.Red;
					color = (ConsoleColor)Enum.Parse(color.GetType(), kv[1]);
					o = color;
				}
				else if (propertyInfo.PropertyType.FullName == "System.Text.Encoding")
				{
					Encoding encoding = null;
					try
					{
						int codePage;
						if (int.TryParse(kv[1], out codePage) == true)
						{
							encoding = Encoding.GetEncoding(codePage);
						}
						else
						{
							encoding = Encoding.GetEncoding(kv[1]);
						}
					}
					catch (Exception ex)
					{
						throw new SystemException("�w�肳��Ă���R�[�h�y�[�W���s���ł��B�R�[�h�y�[�W=" + kv[1], ex);
					}
					o = encoding;

					//Console.OutputEncoding = System.Text.Encoding.UTF8;
					//Console.OutputEncoding = System.Text.Encoding.Unicode;
					//Console.InputEncoding = System.Text.Encoding.UTF8;
					//return 0;
				}
				else if (propertyInfo.PropertyType.FullName == "System.String")
				{
					o = Convert.ToString(kv[1]);
				}
				else
				{
					//erorr
					o = kv[1];
				}

				//
				propertyInfo.SetValue(null, o, null);
				//con.InvokeMember(kv[0],	BindingFlags.SetField, null, null, new object[]{kv[1]});
				return 0;
			}


			Console.WriteLine("�p�����[�^���Ԉ���Ă��܂��B�}�j���A�����m�F���Ă��������B");
			return 1;
		}


	}
}
