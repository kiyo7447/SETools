using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Security.Cryptography;

namespace ALib
{
	/// <summary>
	/// ��A�̃t�@�C���֘A����
	/// </summary>
	public class AFile
	{

		/// <summary>
		/// �w��t�@�C���̃G���R�[�h���擾���܂��B���f�̃��W�b�N�͎������ʂƂȂ�܂��B
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public Encoding GetEncoding(string fileName)
		{
			return ALib.AEncoding.GetEncodingFile(fileName);
		}


		/// <summary>
		/// �w��t�@�C����MD5SUM���擾���܂��B
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public string MD5SUM(string fileName)
		{
			FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

			string s = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(fs)).Replace("-", "").ToLower();

			fs.Close();

			return s;
		}
	}
}
