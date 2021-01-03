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
		/// �ǂݎ���p�t�@�C�����㏑��
		/// </summary>
		bool _isReadOnly = false;

		/// <summary>
		/// ���K�\�����g�p���邩�ۂ�
		/// </summary>
		bool _isRegex = false;

		/// <summary>
		/// �R�[�h�y�[�W���f���W�b�N���g�p���邩�ۂ�
		/// </summary>
		bool _isJudeEncoding = false;

		/// <summary>
		/// �t�@�C���ǂݏ����̃R�[�h�y�[�W
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
			//        throw new ApplicationException("�p�����[�^ �^�C���A�E�g(/w)�̎w�肪����������܂���B�w��͐����ł���K�v������܂��B", ex);
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
			//        throw new ApplicationException("�p�����[�^/s��MaxThreadsCount, CompletionPortThreads������������܂���B�w�肷����e�͂Q�̐����ł���K�v������܂��B", ex);
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

			//�ǂݎ���p�t�@�C�����㏑��
			values = base.GetParameter("R", 0);
			if (values != null)
			{
				_isReadOnly = true;
			}

			//���K�\��
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
					throw new SystemException("�w�肳��Ă���R�[�h�y�[�W���s���ł��B�R�[�h�y�[�W=" + values[0], ex);
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
		/// �ǂݎ���p
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return _isReadOnly;
			}
		}


		/// <summary>
		/// ���K�\���̗L��
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
		/// �R�[�h�y�[�W���f���W�b�N���g�p���邩�ۂ�
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
		/// �t�@�C���ǂݏ����̃R�[�h�y�[�W
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
