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

			//設定ファイルを再読み込み
			_toolStripMenuItemReloadSettingsXML.Click += _toolStripMenuItemReloadSettingsXML_Click;

			//バージョン情報を表示する
			_toolStripMenuItemVersionInfo.Click += _toolStripMenuItemVersionInfo_Click;

			//プログラムの終了
			_toolStripMenuItemEndProgram.Click += new EventHandler(_toolStripMenuItemEndProgram_Click);

			//設定ファイルの読み込み
			LoadSetting();

			mainForm.WindowProcHotKey += new WindowProcHotKeyDelegate(mainForm_WindowProcHotKey);

			//ホットキーを登録する
			_hotKeyManager.RegisterHotKey(_mainForm.Handle);

		}

		/// <summary>
		/// 設定ファイルXMLのリロード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _toolStripMenuItemReloadSettingsXML_Click(object sender, EventArgs e)
		{
			//ホットキーを解除する
			_hotKeyManager.UnregisterHotKey(_mainForm.Handle);

			//設定ファイルの読み込み
			LoadSetting();

			//ホットキーを登録する
			_hotKeyManager.RegisterHotKey(_mainForm.Handle);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _toolStripMenuItemVersionInfo_Click(object sender, EventArgs e)
		{
			var versionForm = new VersionForm();
			versionForm.ShowDialog(_mainForm);
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

			var env = new ALib.AEmviroment();


			_settingInfo.Applications.All(s =>
			{
				//環境変数による書き換えを行う。
				s.CommandInfo.FileName = env.GetParseEmviromentVariables(s.CommandInfo.FileName);
				s.CommandInfo.Arguments = env.GetParseEmviromentVariables(s.CommandInfo.Arguments);
				s.CommandInfo.WorkingDirectory = env.GetParseEmviromentVariables(s.CommandInfo.WorkingDirectory);
				return true;
			});

			//読み込んだ設定ファイルxmlをマネージャに登録する。
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
			//ホットキーを解除する
			_hotKeyManager.UnregisterHotKey(_mainForm.Handle);

			DialogResult ret = _mainForm.ShowDialog();
			if (ret == DialogResult.OK)
			{
				//設定ファイルの読み込み
				LoadSetting();
			}

			//ホットキーを登録する
			_hotKeyManager.RegisterHotKey(_mainForm.Handle);
		}



	}
}
