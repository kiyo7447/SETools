using System;
using System.Collections.Generic;
using System.Text;

namespace SReplace
{
	class Parameter : AbstractParameter
	{

		bool _isHelp = false;
		bool _isDebug = false;
		bool _isEnvironment = false;
		bool _isTimeCopy = false;
		bool _isDeleteLine = false;

		/// <summary>
		/// 読み取り専用ファイルを上書き
		/// </summary>
		bool _isReadOnly = false;

		/// <summary>
		/// 正規表現を使用するか否か
		/// </summary>
		bool _isRegex = false;

		/// <summary>
		/// コードページ判断ロジックを使用するか否か
		/// </summary>
		bool _isJudeEncoding = false;

		/// <summary>
		/// ファイル読み書きのコードページ
		/// </summary>
		Encoding _readEncoding = null;

		string _fromFile = null;
		string _toFile = null;
		string _oldChar = null;
		string _newChar = null;

		public Parameter(string[] args)
			: base(args)
		{
			_SetParameter(args);
		}

		void _SetParameter(string[] args) {
			
			string[] values;

			//values = base.GetParameter("W", 1);
			//if (values != null)
			//{
			//    try
			//    {
			//        _timeOut = int.Parse(values[0]);
			//    }
			//    catch (Exception ex)
			//    {
			//        throw new ApplicationException("パラメータ タイムアウト(/w)の指定が正しくありません。指定は数字である必要があります。", ex);
			//    }
			//}

			//values = base.GetParameter("S", 2);
			//if (values != null)
			//{
			//    try
			//    {
			//        _maxThreads = int.Parse(values[0]);
			//        _completionPortThreads = int.Parse(values[1]);
			//    }
			//    catch (Exception ex)
			//    {
			//        throw new ApplicationException("パラメータ/sのMaxThreadsCount, CompletionPortThreadsが正しくありません。指定する内容は２つの数字である必要があります。", ex);
			//    }
			//}

			values = base.GetParameter("T", 0);
			if (values != null)
			{
				_isTimeCopy = true;
			}

			values = base.GetParameter("E", 0);
			if (values != null)
			{
				_isEnvironment = true;
			}

			values = base.GetParameter("D", 0);
			if (values != null)
			{
				_isDebug = true;
			}

			values = base.GetParameter("?", 0);
			if (values != null)
			{
				_isHelp = true;
			}

			values = base.GetParameter("help", 0);
			if (values != null)
			{
				_isHelp = true;
			}

			values = base.GetParameter("P", 0);
			if (values != null)
			{
				_isJudeEncoding = true;
			}

			values = base.GetParameter("DL", 0);
			if (values != null)
			{
				_isDeleteLine = true;
			}

			//読み取り専用ファイルを上書き
			values = base.GetParameter("R", 0);
			if (values != null)
			{
				_isReadOnly = true;
			}

			//正規表現
			values = base.GetParameter("S", 0);
			if (values != null)
			{
				_isRegex = true;
			}

			values = base.GetParameter("C", 1);
			if (values != null)
			{
				try
				{
					int codePage;
					if (int.TryParse(values[0], out codePage) == true)
					{
						_readEncoding = Encoding.GetEncoding(codePage);
					}
					else
					{
						_readEncoding = Encoding.GetEncoding(values[0]);
					}
				}
				catch (Exception ex)
				{
					throw new SystemException("指定されているコードページが不正です。コードページ=" + values[0], ex);
				}
			}

			values = base.GetParameter(4, 2);
			if (values != null)
			{
				_fromFile = values[0];
				_toFile = values[1];
				if (values.Length >= 4)
				{
					_oldChar = values[2];
					_newChar = values[3];
				}
			}
			
		}


		public string FromFile
		{
			get
			{
				return _fromFile;
			}
		}
		public string ToFile
		{
			get
			{ 
				return _toFile; 
			}
		}
		public string OldChar
		{
			get
			{
				return _oldChar;
			}
		}
		public string NewChar
		{
			get
			{
				return _newChar;
			}
		}

		public bool IsTimeCopy
		{
			get
			{
				return _isTimeCopy;
			}
		}

		public bool IsEnvironment
		{
			get
			{
				return _isEnvironment;
			}
		}

		public bool IsDeleteLine
		{
			get
			{
				return _isDeleteLine;
			}
		}

		/// <summary>
		/// 読み取り専用
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return _isReadOnly;
			}
		}


		/// <summary>
		/// 正規表現の有無
		/// </summary>
		public bool IsRegex
		{
			get
			{
				return _isRegex;
			}
		}

		public bool IsDebug
		{
			get
			{
				return _isDebug;
			}
			set
			{
				_isDebug = value;
			}
		}
		public bool IsHelp
		{
			get
			{
				return _isHelp;
			}
			set
			{
				_isHelp = value;
			}
		}


		/// <summary>
		/// コードページ判断ロジックを使用するか否か
		/// </summary>
		public bool IsEncoding
		{
			get
			{
				return _isJudeEncoding;
			}
			set
			{
				_isJudeEncoding = value;
			}
		}

		/// <summary>
		/// ファイル読み書きのコードページ
		/// </summary>
		public Encoding ReadEncoding
		{
			get
			{
				return _readEncoding;
			}
			set
			{
				_readEncoding = value;
			}
		}

	}
}
