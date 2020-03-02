using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Security.Cryptography;

namespace ALib
{
	/// <summary>
	/// 一連のファイル関連処理
	/// </summary>
	public class AFile
	{

		/// <summary>
		/// 指定ファイルのエンコードを取得します。判断のロジックは自動判別となります。
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public Encoding GetEncoding(string fileName)
		{
			return ALib.AEncoding.GetEncodingFile(fileName);
		}


		/// <summary>
		/// 指定ファイルのMD5SUMを取得します。
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
