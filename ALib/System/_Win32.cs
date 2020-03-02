using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace System
{
	class Win32
	{
		[DllImport("kernel32.dll")]
		public static extern Boolean AttachConsole(uint dwProcessId);

		[DllImport("kernel32.dll")]
		public static extern Boolean FreeConsole();
	}
}
