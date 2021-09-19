using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ALauncher
{
	public class HotKeyInfo
	{
		//XML Serializeに必要
		public HotKeyInfo()
		{

		}

		public HotKeyInfo(Keys key, bool shift, bool control, bool alt, bool windows)
		{
			_key = key;
			_shift = shift;
			_control = control;
			_alt = alt;
			_windows = windows;
		}

		public int Modifiers()
		{
			return (Shift ? Win32.User.MOD_SHIFT : 0) | (Control ? Win32.User.MOD_CONTROL : 0) | (Alt ? Win32.User.MOD_ALT : 0) | (Windows ? Win32.User.MOD_WIN : 0);
		}

		private bool _shift;

		public bool Shift
		{
			get { return _shift; }
			set { _shift = value; }
		}

		private bool _control;

		public bool Control
		{
			get { return _control; }
			set { _control = value; }
		}

		private bool _alt;

		public bool Alt
		{
			get { return _alt; }
			set { _alt = value; }
		}

		private bool _windows;

		public bool Windows
		{
			get { return _windows; }
			set { _windows = value; }
		}

		private Keys _key;

		public Keys Key
		{
			get { return _key; }
			set { _key = value; }
		}

		public override string ToString()
		{
			string r = "";
			if (Windows) r += "+Win";
			if (Control) r += "+Ctrl";
			if (Alt) r += "+Alt";
			if (Shift) r += "+Shift";
			r += "+" + Key;
			return r.Substring(1);
		}
	}
}
