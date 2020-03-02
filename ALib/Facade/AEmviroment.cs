using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ALib
{
	/// <summary>
	/// ���ϐ��֘A��Facade�N���X�Q
	/// </summary>
	public class AEmviroment
	{
		/// <summary>
		/// ����������ϐ����g���đS�Ēu�����܂��B
		/// </summary>
		/// <param name="targetString"></param>
		/// <returns></returns>
		public string GetParseEmviromentVariables(string targetString)
		{
			string retString;
			retString = targetString;
			IEnumerator e = System.Environment.GetEnvironmentVariables().GetEnumerator();
			while (e.MoveNext())
			{
				DictionaryEntry dic = (DictionaryEntry)e.Current;

				retString = retString.Replace("%" + dic.Key.ToString() + "%", dic.Value.ToString());
			}
			return retString;
		}
	}
}
