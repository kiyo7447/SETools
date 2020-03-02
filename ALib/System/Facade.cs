using System;
using System.Collections.Generic;
using System.Text;
using ALib;

namespace System
{
	/// <summary>
	/// �X�^�e�B�b�N�@�\�̌Ăяo����
	/// </summary>
	public static class Facade
	{
		static AFile _aFile = null;
		static WakeOnLan _wakeOnLan = null;
		static AbstractMessage _message = null;
		static AEmviroment _emviroment = null;

		/// <summary>
		/// ��A�̃t�@�C���֘A����
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
		/// WakeOnLan�֘A�̏���
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
		/// ���[�U�K�C�_���X�֘A�̏���
		/// </summary>
		public static AbstractMessage Message
		{
			get
			{
				if (_message == null)
				{
					//if (Context.ApplicationType == ApplicationType.ConsoleApplication)
					//{
						//TODO:�R���\�[�����E�B���h�E���̔��f���K�v
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
		/// ���ϐ��֘A�̏���
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
