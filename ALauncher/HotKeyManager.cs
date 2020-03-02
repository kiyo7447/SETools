using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace ALauncher
{
	class HotKeyManager
	{
		static SettingInfo _settingInfo;

		public HotKeyManager(SettingInfo settingInfo)
		{
			for (int c = 0; c < settingInfo.Applications.Count; c++)
			{
				settingInfo.Applications[c].Id = c;
			}
			_settingInfo = settingInfo;
		}

		public ApplicationModel RegisterHotKey(IntPtr windowHandle)
		{
			ApplicationModel ret = null;

			_settingInfo.Applications.All((am) =>
			{
				int i = Win32.User.RegisterHotKey(windowHandle, am.Id, am.HotKeyInfo.Modifiers(), (int)am.HotKeyInfo.Key);

				Debug.WriteLine("RegisterHotKey:{0}", new object[] { am.HotKeyInfo.ToString() });
				if (i == 0)
				{
					MessageBox.Show(string.Format("「{0}」のホットキー「{1}」をシステムに登録できません。", am.Name, am.HotKeyInfo.ToString()), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				}
				return true;
			});

			return ret;
		}

		public void UnregisterHotKey(IntPtr windowHandle)
		{
			_settingInfo.Applications.All((am) =>
			{
				Debug.WriteLine("UnregisterHotKey:{0}", new object[] { am.HotKeyInfo.ToString() });
				int i = Win32.User.UnregisterHotKey(windowHandle, am.Id);
				return true;
			});
		}

		public void WinProc(int wParam)
		{
			int id = wParam;

			ApplicationModel applicationModel = _settingInfo.Applications[id];

			ProcessStartInfo psi = applicationModel.CommandInfo.GetProcessStartInfo();


			applicationModel.ProcessStartInfo.Add(psi);
			Process p = null;
			try
			{



				//Process p = _settingInfo.Applications[id].LastProcess;
				//if (p != null)
				//{
				//    Win32.User.SetForegroundWindow(p.MainWindowHandle);
				//}
				//else
				//{
				//    _settingInfo.Applications[id].LastProcess = Process.Start(psi);
				//}
				//psi.CreateNoWindow = true;
				//psi.UseShellExecute = false;
				p = Process.Start(psi);

				// Thread.Sleep(100);

				if (applicationModel.CommandInfo.SetForegroundWindow == true)
				{
					//フラグ制御でフォーカスセット
					Win32.User.SetForegroundWindow(p.MainWindowHandle);
				}


			}
			catch (Exception ex)
			{


				MessageBox.Show(null,
					"アプリケーションの起動でエラーが発生しました。" +
					"\n\nKey=" + applicationModel.HotKeyInfo.Key +
					"\nControl=" + applicationModel.HotKeyInfo.Control +
					"\nAlt=" + applicationModel.HotKeyInfo.Alt +
					"\nWindows=" + applicationModel.HotKeyInfo.Windows +
					"\n\nFileName=" + psi.FileName +
					"\nArguments=" + psi.Arguments + "\n\nエラー詳細=" + ex.ToString(), "エラー");
			}
		}
	}
}
