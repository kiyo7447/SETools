using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;

namespace ALauncher
{
	public delegate void WindowProcHotKeyDelegate(int KeyCode);


	public partial class MainForm : Form
	{
		public event WindowProcHotKeyDelegate WindowProcHotKey;

		public MainForm()
		{
			InitializeComponent();
		}
		

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == Win32.User.WM_HOTKEY)
			{
				WindowProcHotKey((int)m.WParam);
			}
		}

		/// <summary>
		/// Fromの使いまわしはできません。
		/// http://msdn.microsoft.com/ja-jp/library/system.windows.forms.form.close.aspx
		/// の通り、Hideを代替メソッドとして使用して、再使用可能な状態で閉じます。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

	}
}
