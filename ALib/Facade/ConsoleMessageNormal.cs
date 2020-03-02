using System;
using System.Collections.Generic;
using System.Text;

namespace ALib
{
	public class ConsoleMessageNormal : AbstractMessageNormal
	{

		public override void Show(string msg)
		{
			Console.WriteLine(msg);
		}
	}
}
