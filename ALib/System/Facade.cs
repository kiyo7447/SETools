using System;
using System.Collections.Generic;
using System.Text;
using ALib;

namespace System
{
	/// <summary>
	/// スタティック機能の呼び出し口
	/// </summary>
	public static class Facade
	{
		static AFile _aFile = null;
		static WakeOnLan _wakeOnLan = null;
		static AbstractMessage _message = null;
		static AEmviroment _emviroment = null;

		/// <summary>
		/// 一連のファイル関連処理
		/// </summary>
		public static AFile File
		{
			get
			{
				if (_aFile == null)
				{
					_aFile = new AFile();
				}
				return _aFile;
			}
		}

		/// <summary>
		/// WakeOnLan関連の処理
		/// </summary>
		public static WakeOnLan WakeOnLan
		{
			get
			{
				if (_wakeOnLan == null)
				{
					_wakeOnLan = new WakeOnLan();
				}
				return _wakeOnLan;
			}
		}

		/// <summary>
		/// ユーザガイダンス関連の処理
		/// </summary>
		public static AbstractMessage Message
		{
			get
			{
				if (_message == null)
				{
					//if (Context.ApplicationType == ApplicationType.ConsoleApplication)
					//{
						//TODO:コンソールかウィンドウかの判断が必要
						_message = new ConsoleMessage();
					//}
					//else
					//{
					//    _message = new ApplicationMessage();
					//}
				}
				return _message;
			}
		}

		/// <summary>
		/// 環境変数関連の処理
		/// </summary>
		public static AEmviroment Emviroment
		{
			get
			{
				if (_emviroment == null)
				{
					_emviroment = new AEmviroment();
				}
				return _emviroment;
			}
		}

	}
}
