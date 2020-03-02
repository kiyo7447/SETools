using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ALauncher
{
	public partial class MainComponent : Component
	{
		SettingInfo _settingInfo;

		HotKeyManager _hotKeyManager;

		Form _mainForm;

		public MainComponent()
		{
			InitializeComponent();
		}

		public MainComponent(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public void Initialize(MainForm mainForm)
		{
			_mainForm = mainForm;
			//_hotKeyManager = 

			//設定画面を開く
			_notifyIcon.DoubleClick += new EventHandler(_notifyIcon_DoubleClick);

			//プログラムの終了
			_toolStripMenuItemEndProgram.Click += new EventHandler(_toolStripMenuItemEndProgram_Click);


			//設定ファイルの読み込み
			LoadSetting();

			mainForm.WindowProcHotKey += new WindowProcHotKeyDelegate(mainForm_WindowProcHotKey);


			_hotKeyManager.RegisterHotKey(_mainForm.Handle);		
			
		}

		void mainForm_WindowProcHotKey(int keyCode)
		{
			_hotKeyManager.WinProc(keyCode);
		}

		public void EndProc()
		{
			_hotKeyManager.UnregisterHotKey(_mainForm.Handle);
		}

		public void LoadSetting()
		{
			//XMLの読み込み
			FileInfo settingFile = new FileInfo(Application.ExecutablePath + ".xml");

			try
			{
				_settingInfo = SettingInfo.Deserialize(settingFile);

			}
			catch (Exception ex)
			{

				throw new SystemException("設定ファイルの読み込みに失敗しました。FileName=" + settingFile, ex);
			}
				_hotKeyManager = new HotKeyManager(_settingInfo);
		}

		/// <summary>
		/// プログラムの終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _toolStripMenuItemEndProgram_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// 設定画面を開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			_hotKeyManager.UnregisterHotKey(_mainForm.Handle);

			
			DialogResult ret =  _mainForm.ShowDialog();
			if (ret == DialogResult.OK)
			{
				LoadSetting();
			}
			_hotKeyManager.RegisterHotKey(_mainForm.Handle);
		}



	}
}
