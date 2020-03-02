using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	/// <summary>
	/// �p�����[�^�̊��N���X
	/// </summary>
	public abstract class AbstractParameter : IProperty
	{
		List<string> _argumentsExceptOptions = new List<string>();
		
		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="args">�v���O�����̋N���p�����[�^</param>
		public AbstractParameter(string[] args)
		{
			foreach(string arg in args)
			{
				_argumentsExceptOptions.Add(arg);
			}
		}


		/// <summary>
		/// 
		/// getDataCount		0		=		���݂̃f�[�^��S�ĕԂ�
		/// getDataCount		n		=		���݂̃f�[�^����getDataCount�̏������ق���Ԃ�
		/// 
		/// requireDataCount	0		=		�p�����[�^�̐��̃`�F�b�N���s��Ȃ�
		/// requireDataCount	n		=		�p�����[�^�̐��̃`�F�b�N
		/// </summary>
		/// <param name="getDataCount">0:������</param>
		/// <param name="requireDataCount">0:�p�����[�^�Ȃ�������</param>
		/// <returns></returns>
		protected string[] GetParameter(int getDataCount, int requireDataCount)
		{
			//�K�{���̓`�F�b�N
			if (requireDataCount != 0 && _argumentsExceptOptions.Count < requireDataCount)
			{
				throw new ApplicationException("�K�{�p�����[�^�̐�������Ă��܂���B�K�v�ȕK�{�p�����[�^��=" + requireDataCount.ToString() + "�A�K�{�w�肳�ꂽ�p�����[�^�̐�=" + _argumentsExceptOptions.Count + "�A�p�����[�^�̓��e=" + Dump.GetMessage(_argumentsExceptOptions) + "�A�ڍׂ̓w���v���Q�Ƃ��Ă��������B");
			}

			int dataCount;
			if (getDataCount == 0)
			{
				//�S����Ԃ�
				dataCount = _argumentsExceptOptions.Count;
			}
			else
			{
				//�ő�l�����߂Ă��錏���Ńf�[�^��Ԃ�
				if (_argumentsExceptOptions.Count > getDataCount)
				{
					dataCount = getDataCount;
				}
				else
				{
					dataCount = _argumentsExceptOptions.Count;
				}
			}

			string[] ret = new string[dataCount];
			for (int idxRet = 0; idxRet < dataCount; idxRet++)
			{
				ret[idxRet] = _argumentsExceptOptions[0];
				_argumentsExceptOptions.RemoveAt(0);
			}
			return ret;
		}


		/// <summary>
		/// �p�����[�^���擾���܂��B
		/// </summary>
		/// <param name="key"></param>
		/// <param name="getMaxDataCount">�O�F�ő吔�͌��܂��Ă��܂���B</param>
		/// <param name="requireDataCount">�K�v�ȍŒ���̃p�����[�^��</param>
		/// <returns></returns>
		protected string[] GetParameter(string key, int getMaxDataCount, int requireDataCount)
		{
			int idx;
			List<string> retO = new List<string>();
			for(idx = 0; idx < _argumentsExceptOptions.Count; idx++) 
			{
				bool existsFlg = false;
				if (_argumentsExceptOptions[idx].ToUpper() == "/" + key.ToUpper())
				{
					existsFlg = true;
				}
				else if (_argumentsExceptOptions[idx].ToUpper() == "-" + key.ToUpper())
				{
					existsFlg = true;
				}

				if (existsFlg == true)
				{
					break;
				}
			}
			if (idx == _argumentsExceptOptions.Count)
			{
				//�p�����[�^�Ȃ�
				if (requireDataCount > 0)
				{
					throw new SystemException(string.Format("�p�����[�^/{0}�́A�K�v�̃p�����[�^����{1}�K�v�ł����A���������܂���ł����B", new object[] { key, requireDataCount}));
				}
				return null;
			}
			else
			{
				//�p�����[�^����i�L�[���폜�j
				_argumentsExceptOptions.RemoveAt(idx);

				int maxDataCount = getMaxDataCount==0?int.MaxValue:getMaxDataCount;
				for (int idxRet = 0; idxRet < maxDataCount; idxRet++)
				{
					//��͂���ׂ��L�[���Ȃ��ꍇ�͉�͂̏I��
					if (idx >= _argumentsExceptOptions.Count)
					{
						break;
					}

					//���̃L�[������ꍇ�͉�͂��I��
					if (_argumentsExceptOptions[idx].Substring(0, 1) == "-" || _argumentsExceptOptions[idx].Substring(0, 1) == "/")
					{
						break;
					}


					retO.Add(_argumentsExceptOptions[idx]);

					//������͍폜
					_argumentsExceptOptions.RemoveAt(idx);
				}

				//�K�{���̃L�[�����������̂����`�F�b�N
				if (requireDataCount > 0 && retO.Count < requireDataCount)
				{
					throw new SystemException(string.Format("�p�����[�^/{0}�ɑ��������̐�������Ă��܂���B�K�v�ȃp�����[�^�̐��́A{1}�ł����A{2}����������܂���ł����B", new object[]{key, requireDataCount, retO.Count}));
				}

				string[] ret = new string[retO.Count];
				for(int i = 0;i < retO.Count;i++) {
					ret[i] = retO[i];
				}
				return ret;
			}

		}

		/// <summary>
		/// �r���L�[�̎擾���s���܂��B
		/// </summary>
		/// <param name="key"></param>
		/// <param name="dataCount"></param>
		/// <returns>���p�����[�^=�L�[�A���p�����[�^�ȍ~�̓f�[�^</returns>
		protected string[] GetParameterExclusive(string[] key, int dataCount)
		{
			return null;
		}

		/// <summary>
		/// �p�����[�^�̉�͏���
		/// </summary>
		/// <param name="key">�擾����p�����[�^�̃L�[</param>
		/// <param name="dataCount">�擾����p�����[�^�̐�</param>
		/// <returns></returns>
		protected string[] GetParameter(string key, int dataCount)
		{
			int idx;
			for(idx = 0; idx < _argumentsExceptOptions.Count; idx++) 
			{
				bool existsFlg = false;
				if (_argumentsExceptOptions[idx].ToUpper() == "/" + key.ToUpper())
				{
					existsFlg = true;
				}
				else if (_argumentsExceptOptions[idx].ToUpper() == "-" + key.ToUpper())
				{
					existsFlg = true;
				}

				if (existsFlg == true)
				{
					break;
				}
			}
			if (idx == _argumentsExceptOptions.Count)
			{
				//�p�����[�^�Ȃ�
				return null;
			}
			else
			{
				//�p�����[�^����
				_argumentsExceptOptions.RemoveAt(idx);

				string[] ret = new string[dataCount];
				for (int idxRet = 0; idxRet < dataCount; idxRet++)
				{
					if (idx >= _argumentsExceptOptions.Count)
					{
						throw new ApplicationException("�p�����[�^/" + key + "�ɑ�������������܂���B�����𐳂����w�肵�Ă��������B");
					}
					ret[idxRet] = _argumentsExceptOptions[idx];
					_argumentsExceptOptions.RemoveAt(idx);
				}

				return ret;
			}
		}

		//public List<string> ArgumentsExceptOptions
		//{
		//    get
		//    {
		//        return _argumentsExceptOptions;
		//    }
		//}

		protected Encoding GetEncoding(string encodingString)
		{
			return ALib.AEncoding.GetEncodingString(encodingString);
		}
	}
}
