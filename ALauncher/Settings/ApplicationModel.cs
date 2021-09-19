using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ALauncher
{
	public class ApplicationModel
	{
		//XML Serializeに必要
		public ApplicationModel()
		{
		}

		public ApplicationModel(int id, string name, HotKeyInfo hotKeyInfo, CommandInfo commandInfo)
		{
			_id = id;
			_name = name;
			_hotKeyInfo = hotKeyInfo;
			_commandInfo = commandInfo;
		}

		private int _id;

		/// <summary>
		/// RegisterHotKeyのIDに使用します。（１～）
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _name;

		/// <summary>
		/// 表示コマンド名称
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private HotKeyInfo _hotKeyInfo;

		public HotKeyInfo HotKeyInfo
		{
			get { return _hotKeyInfo; }
			set { _hotKeyInfo = value; }
		}

		private List<ProcessStartInfo> _processStartInfo = new List<ProcessStartInfo>();

		[XmlIgnore]
		public List<ProcessStartInfo> ProcessStartInfo
		{
			get { return _processStartInfo; }
			set { _processStartInfo = value; }
		}


		private CommandInfo _commandInfo;

		public CommandInfo CommandInfo
		{
			get { return _commandInfo; }
			set { _commandInfo = value; }
		}

		//private Process _lastProcess = null;

		//[XmlIgnore]
		//public Process LastProcess
		//{
		//    get { return _lastProcess; }
		//    set { _lastProcess = value; }
		//}

	}
}
