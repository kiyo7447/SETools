using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ALib
{
	public class ConsoleMessageError : AbstractMessageError
	{
		public override void Show(Exception ex)
		{
			Trace.WriteLine(Dump.GetExceptionMessageForDeveloper(ex).ToString());

			if (Context.IsDebug == true)
			{
				Show(Dump.GetExceptionMessageForDeveloper(ex).ToString());
			}
			else
			{
				Show(Dump.GetExceptionMessageForConsumer(ex));
			}


		}
		public override void Show(string msg)
		{
			string errorMesssage = "";
			if (Context.IsDebug == true)
			{
				errorMesssage = 
					DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff(ddd)") + Environment.NewLine + 
					"Context=" + Context.ToString() + Environment.NewLine +
					msg;
				Trace.WriteLine(errorMesssage);

			}
			else
			{
				errorMesssage = msg;
				Trace.WriteLine(errorMesssage);

			}
			ConsoleColor cc = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine(errorMesssage);
			Console.ForegroundColor = cc;
		}
	}
}
