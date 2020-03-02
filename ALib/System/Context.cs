using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	public class Context
	{

		static bool _isDebug = false;

		public static bool IsDebug
		{
			get
			{
				return _isDebug;
			}
			set
			{
				_isDebug = value;
			}
		}


		public static new  string ToString()
		{
			Dictionary<string, object> dump = new Dictionary<string, object>();


			dump.Add("IsDebug", _isDebug);



			return Dump.GetMessage(dump);
		}

		static bool _isHelp = false;

		public static bool IsHelp
		{
			get
			{
				return _isHelp;
			}
			set
			{
				_isHelp = value;
			}
		}


		//Windowsアプリケーションがコンソールに出力するサンプル
		//ただし、本当のコンソールアプリケーションの場合は、これが効かない。・・・無意味
		//public static ApplicationType ApplicationType
		//{
		//    get
		//    {
		//        // 自ﾌﾟﾛｾｽを親ﾌﾟﾛｾｽのｺﾝｿｰﾙにｱﾀｯﾁする｡
		//        bool ret = Win32.AttachConsole(System.UInt32.MaxValue);
		//        if (ret)
		//        {

		//            System.IO.StreamWriter stdout = new System.IO.StreamWriter(System.Console.OpenStandardOutput());

		//            stdout.AutoFlush = true;

		//            System.Console.SetOut(stdout);

		//            System.Console.WriteLine("\nProgram started as GUI program in console.\n");

		//            Win32.FreeConsole();

		//            return ApplicationType.ConsoleApplication;
		//        }
		//        else
		//        {
		//            return ApplicationType.WindowsFormApplication;
		//        }
		//    }
		//}
	}
}
