using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ALauncher
{
	/// <summary>
	/// ApplicationModel
	///     +HotKyeInfo
	///     +CommandInfo
	/// </summary>
	public class CommandInfo
	{
		//XML Serializeに必要
		public CommandInfo()
		{
		}
		public CommandInfo(string fileName, string arguments, ProcessWindowStyle processWindowStyle, string workingDirectory, string a)
		{
			_fileName = fileName;
			_arguments = arguments;
			_processWindowStyle = processWindowStyle;
			_workingDirectory = workingDirectory;
		}

		public ProcessStartInfo GetProcessStartInfo()
		{
			ProcessStartInfo processStartInfo = null;
			if (FileName.ToUpper() == "{RUNCLIPBOARDCOMMAND}")
			{
				string clipboardExecFileName;
				try
				{
					clipboardExecFileName = Clipboard.GetText();
					clipboardExecFileName = clipboardExecFileName.Replace("\n", "").Replace("\r", "");
					processStartInfo = new ProcessStartInfo(clipboardExecFileName, Arguments);
				}
				catch (Exception ex)
				{
					throw new SystemException("クリップボードからの実行コマンドの取得に失敗しました。", ex);
				}
			}
			else
			{
				processStartInfo = new ProcessStartInfo(FileName, Arguments);
			}

			processStartInfo.WindowStyle = ProcessWindowStyle;
			processStartInfo.WorkingDirectory = WorkingDirectory;

			processStartInfo.CreateNoWindow = CreateNoWindow;
			processStartInfo.UseShellExecute = UseShellExecute;

			//http://code.msdn.microsoft.com/windowsdesktop/CVB-NET-Framework-1582bbca
			//Shellでの実行時しか管理者権限に昇格できません。
			if (UseShellExecute == true && IsRunas == true)
			{
				processStartInfo.Verb = "runas";
			}



			return processStartInfo;
		}


		private string _fileName;

		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}


		private string _arguments;

		public string Arguments
		{
			get { return _arguments; }
			set { _arguments = value; }
		}



		private ProcessWindowStyle _processWindowStyle;

		public ProcessWindowStyle ProcessWindowStyle
		{
			get { return _processWindowStyle; }
			set { _processWindowStyle = value; }
		}



		private string _workingDirectory;

		public string WorkingDirectory
		{
			get { return _workingDirectory; }
			set { _workingDirectory = value; }
		}


		private bool _createNoWindow = false;
		public bool CreateNoWindow
		{
			get { return _createNoWindow; }
			set { _createNoWindow = value; }
		}


		private bool _useShellExecute = true;
		public bool UseShellExecute
		{
			get { return _useShellExecute; }
			set { _useShellExecute = value; }
		}

		private bool _setForegroundWindow = false;
		public bool SetForegroundWindow
		{
			get { return _setForegroundWindow; }
			set { _setForegroundWindow = value; }
		}


		private bool _isRunas = false;
		public bool IsRunas
		{
			get { return _isRunas; }
			set { _isRunas = value; }
		}
	}
}
