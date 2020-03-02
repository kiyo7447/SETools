using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace ALib
{
	internal static class AEncoding
	{
		/// <summary>
		/// �������Encoding�I�u�W�F�N�g�ɕϊ����鎸�s�����ꍇ�́A
		/// </summary>
		/// <param name="encString">�������������w��</param>
		/// <returns>Encoding�I�u�W�F�N�g</returns>
		internal static Encoding GetEncodingString(string encString)
		{
			Encoding encoding = null;
			try
			{
				int codePage;
				if (int.TryParse(encString, out codePage) == true)
				{
					encoding = Encoding.GetEncoding(codePage);
				}
				else
				{
					encoding = Encoding.GetEncoding(encString);
				}
			}
			catch (Exception ex)
			{
				throw new SystemException("�w�肳��Ă���R�[�h�y�[�W���s���ł��B�R�[�h�y�[�W=" + encString, ex);
			}

			return encoding;
		}


		/// <summary>
		/// �Ώۃt�@�C���̃G���R�[�f�B���O���擾
		/// �쐬���ł��B�b�胍�W�b�N�̂ݎ���
		/// 
		/// 
		/// ���͐��x�������A�����Q�l�ɍ��ς����ق�������
		/// http://dobon.net/vb/dotnet/string/detectcode.html
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static Encoding GetEncodingFile(string fileName)
		{
			//TODO: UTF-8N�̂ݑΉ�
			//���������W�b�N�́A
			//http://kasumi.sakura.ne.jp/~gm/gpj/dev/tips/other/kanji.shtml

			//��Ɋg�����A�e�L�X�g�t�@�C������́A�o�Ă��镶����
			//���͂��A���v�I�ɔ��f�����܂����B

			byte[] bytes = File.ReadAllBytes(fileName);

			//UTF-8N���H�i�܂�BOM���ۂ��Ŕ��f�j�擪���Aefbbbf
			if (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
			{
				return Encoding.UTF8;
			}
			//BOM�t��UTF-16���g���G���f�B�A��
			else if (bytes[0] == 0xFF && bytes[1] == 0xFE)
			{
				return Encoding.Unicode;
			}
			//BOM�t��UTF-16�r�b�O�G���f�B�A��
			else if (bytes[0] == 0xFE && bytes[1] == 0xFF)
			{
				return Encoding.BigEndianUnicode;
			}
			else
			{
				//BOM�������ꍇ�́A�}�j�A�b�N�ɒ��ׂ�B���v�I��99%�͉�͉\���Ǝv���B
				return _DetectEncoding(bytes);
			}
		}

		private static Encoding _DetectEncoding(string fileName)
		{
			byte[] byt = File.ReadAllBytes(fileName);

			//return _DetectEncoding(b);


			FileStream stm = new FileStream(fileName, FileMode.Open);

			byte[] b = new byte[stm.Length];

			stm.Read(b, 0, (int)stm.Length);

			stm.Close();

			return _DetectEncoding(b);
		}


		/// <summary>
		/// ���_������ABOM�Ȃ���UTF-16��UTF-16 Big-Endian���Ή����؂�Ă��Ȃ��B
		///		�G�ۂ悤�Ƀ��[�U�ɑI�΂���ׂ��B
		/// </summary>
		/// <param name="innerBytes"></param>
		/// <returns></returns>
		private static Encoding _DetectEncoding(byte[] innerBytes)
		{
			Encoding retEncode;

			// �����R�[�h���������肷��
			// �Y�������R�[�h�ł͂��肦�Ȃ����Ƃ������t���O�Q
			bool isNotUTF8 = false;
			bool isNotSJIS = false;
			bool isNotEUC = false;
			// �O�̕�����ۑ�����X�^�b�N
			Stack UTF8Stack = new Stack();
			Stack JISStack = new Stack();
			Stack SJISStack = new Stack();
			Stack EUCStack = new Stack();
			// ���̕����R�[�h�ƌ��Ȃ����Ƃ��̕����̐�
			int UTF8Count = 0;
			int SJISCount = 0;
			int EUCCount = 0;
			// �S�o�C�g�����[�v�����i-2����̂̓o�C�g��ɏI�[�́u\0�v���܂܂�邽�߁j
			for (int cnt = 0; cnt <= innerBytes.Length - 2; cnt++)
			{
				// 1�o�C�g�ǂ�
				byte c1 = innerBytes[cnt];
				// &H00�Ȃ�Unicode�m��
				if (c1 == 0)
				{
					retEncode = Encoding.Unicode;
					return retEncode;
				}
				// JIS�G�X�P�[�v�V�[�P���X�̓o��𒲂ׂ�
				switch (c1)
				{
					case 27:
						// �G�X�P�[�v�V�[�P���X�̂͂��܂�
						JISStack.Push(c1);
						break;
					case 36:
					case 40:
						// �u$�v�܂��́u(�v
						if (JISStack.Count != 0)
						{
							// �O�̃o�C�g��&H1B�iESC�j���ǂ���
							if (((byte)JISStack.Peek()) == 27)
							{
								// ���̃o�C�g��ۑ�
								JISStack.Push(c1);
							}
						}
						break;
					case 64:
					case 66:
					case 74:
						// �uB�v�uJ�v�u@�v
						if (JISStack.Count == 2)
						{
							// �O��2�o�C�g���擾
							byte c2 = (byte)JISStack.Pop();
							byte c3 = (byte)JISStack.Pop();
							// �O���G�X�P�[�v�V�[�P���X���ǂ���
							if (((c2 == 36) | (c2 == 40)) & (c3 == 27))
							{
								// �G�X�P�[�v���o�Ă�����AJIS�Ƃ��Ċm��
								retEncode = Encoding.GetEncoding("ISO-2022-JP");
								return retEncode;
							}
						}

						break;
				}
				// �V�t�gJIS���ǂ����A�����āA�z�肳��镶�����𒲂ׂ�
				if ((SJISStack.Count == 0))
				{
					// 1�o�C�g��
					if (((c1 >= 129) & (c1 <= 159)) | ((c1 >= 224) & (c1 <= 252)))
					{
						// ����1�o�C�g��
						SJISStack.Push(c1);
					}
				}
				else if ((SJISStack.Count == 1))
				{
					// 2�o�C�g�ڂ̔���
					if (((c1 >= 64) & (c1 <= 126)) | ((c1 >= 128) & (c1 <= 252)))
					{
						byte c2 = (byte)SJISStack.Pop();
						// �O�o�C�g���L���ȃV�t�gJIS��1�o�C�g��
						SJISCount += 1;
					}
					else
					{
						// �V�t�gJIS�ł͂Ȃ�
						isNotSJIS = true;
					}
				}
				// EUC���ǂ����A�����āA�z�肳��镶�����𒲂ׂ�
				if (c1 == 142)
				{
					// ���p�J�i�̃}�[�N�ł���
					EUCStack.Push(c1);
				}
				if (((c1 >= 161) & (c1 <= 254)))
				{
					// �����ł���
					if ((EUCStack.Count != 0))
					{
						// ��s�o�C�g����
						byte c2 = (byte)EUCStack.Pop();
						if ((c2 != 142))
						{
							// ���p�J�i�ł͂Ȃ�
							EUCCount += 1;
						}
					}
					else
					{
						// 1�o�C�g��
						EUCStack.Push(c1);
					}
				}

				// UTF-8���ǂ����A�����āA�z�肳��镶�����𒲂ׂ�
				if ((c1 >= 192 && c1 <= 223) || (c1 >= 224 && c1 <= 239))
				{
					// UTF-8��2�o�C�g�R�[�h�܂���3�o�C�g�R�[�h��1�o�C�g��
					// ���̃o�C�g��ۑ�
					UTF8Stack.Push(c1);
				}
				else if (c1 >= 128 && c1 <= 191)
				{
					// UTF-8��2�o�C�g�ڂ܂���3�o�C�g��
					if (UTF8Stack.Count == 1)
					{
						// �O1�o�C�g���擾
						byte c2 = (byte)UTF8Stack.Pop();
						if ((c2 >= 192) & (c2 <= 223))
						{
							// UTF-8�Ƃ��Đ�����
							UTF8Count += 1;
						}
						else if ((c2 >= 224) & (c2 <= 239))
						{
							// ����1�o�C�g����
							UTF8Stack.Push(c2);
							UTF8Stack.Push(c1);
						}
						else
						{
							// UTF-8�ł͂Ȃ�
							isNotUTF8 = true;
						}
					}
					else if (UTF8Stack.Count == 2)
					{
						// �O2�o�C�g���擾
						byte c2 = (byte)UTF8Stack.Pop();
						byte c3 = (byte)UTF8Stack.Pop();
						// 2�o�C�g�ڂ�&H80�`&HBF��
						// 1�o�C�g�ڂ�&HE0�`&HEF�Ȃ�UTF8�Ƃ��Đ�����
						if ((c2 >= 128) & (c2 <= 191) & (c3 >= 224) & (c3 <= 239))
						{
							UTF8Count += 1;
						}
						else
						{
							// UTF8�ł͂Ȃ�
							isNotUTF8 = true;
						}
					}
				}
			}
			// �ȉ��A���v�������ƂɁA�v�킵�������R�[�h�𔻒�
			// Unicode��JIS�̏ꍇ�ɂ́A��L���[�v���Ŋm�肵�Ă���̂ŁA���̏����ɂ͗��Ȃ�
			// �c���UTF-8�A�V�t�gJIS�AEUC
			// �t���O�Ɠo��p�x�Ō��߂�
			if (isNotUTF8) UTF8Count = 0;
			if (isNotSJIS) SJISCount = 0;
			if (isNotEUC) EUCCount = 0;
			if (UTF8Count > Math.Max(SJISCount, EUCCount))
			{
				retEncode = Encoding.UTF8;
				return retEncode;
			}
			if (SJISCount > Math.Max(UTF8Count, EUCCount))
			{
				retEncode = Encoding.GetEncoding("Shift_JIS");
				return retEncode;
			}
			if (EUCCount > Math.Max(UTF8Count, SJISCount))
			{
				retEncode = Encoding.GetEncoding("EUC-JP");
				return retEncode;
			}
			// �m��ł��Ȃ����UTF-8�ɂ���
			retEncode = Encoding.UTF8;
			return retEncode;
		}

	}
}
