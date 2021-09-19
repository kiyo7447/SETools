using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ALib
{
	/// <summary>
	/// 環境変数関連のFacadeクラス群
	/// </summary>
	public class AEmviroment
	{
		/// <summary>
		/// 文字列を環境変数を使って全て置換します。
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
