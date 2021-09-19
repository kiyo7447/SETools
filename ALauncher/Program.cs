using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ALauncher
{
	static class Program
	{
		static MainComponent _mc;
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				_mc = new MainComponent();
				MainForm mf = new MainForm();

				//終了処理
				Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

				//初期化
				_mc.Initialize(mf);

				Application.Run();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}
		}

		static void Application_ApplicationExit(object sender, EventArgs e)
		{
			//終了処理をコンポーネントに通知
			_mc.EndProc();
		}
	}
}
