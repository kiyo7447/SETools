using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace System
{
	/// <summary>
	/// デバッグメッセージを構築するための一連の処理です。
	/// ArrayType			{"a","b","c","d"}								String型
	/// ArrayType			{1,2,3,4}										String型以外のPrimitiv型
	/// DictionaryType		{name="value",name="value",name="value"}		String型
	/// DictionaryType		{name=value,name=value,name=value}				String型以外のPrimitiv型
	/// ObjectType			{name=[Type]value,name=[Type]value,name=[Type]value}
	/// </summary>
	public class Dump
	{
		/*
		private const string START_LINE_KEY = "<mid" + ">";
		private const string END_LINE_KEY = "</mid" + ">";
		*/

		/// <summary>
		/// オブジェクトのデバックメッセージを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="buf"></param>
		public static void GetMessage(object obj, StringBuilder buf) 
		{
			_GetMessage(obj, buf);
		}

		/// <summary>
		/// オブジェクトのデバックメッセージを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetMessage(object obj) 
		{
			StringBuilder buf = new StringBuilder(1024);
			GetMessage(obj, buf);
			return buf.ToString();
		}

		/// <summary>
		/// ExceptinのToString()メソッドの代わりに使用します。
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static string GetExceptionMessageForConsumer(Exception ex)
		{
			return _GetExceptipnMessage(ex);
		}

		/// <summary>
		/// ExceptinのToString()メソッドの代わりに使用します。
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static StringBuilder GetExceptionMessageForDeveloper(Exception ex)
		{
			StringBuilder buf = new StringBuilder(1024);
			buf.AppendLine(ex.ToString().Replace("--->", Environment.NewLine + "-->"));
			buf.AppendLine("---詳細");
			GetPropertyMessage(ex, buf, new string[] { "StackTrace" });
			return buf;
		}

		/// <summary>
		/// オブジェクトのプロパティのデバックメッセージを取得します。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="buf"></param>
		/// <param name="leaveName"></param>
		internal static void GetPropertyMessage(object obj, StringBuilder buf, string[] leaveName)
		{
			if (obj == null)
			{
				buf.Append("null");
			}


			bool firstFlg = false;
            buf.Append("{");
            foreach (PropertyInfo property in obj.GetType().GetProperties())
			{
				if (firstFlg == false)
				{
					firstFlg = true;
				}
				else
				{
					buf.Append(",");
				}

				if (leaveName != null)
				{
					int idx = 0;
					for (;idx <leaveName.Length;idx++)
					{
						if (leaveName[idx] == property.Name)
						{
							break;
						}
					}
					if (idx != leaveName.Length)
					{
						buf.Append(property.Name + "=この内容はコード(実装)により、表示されません。");
						continue;
					}
				}

				object value = property.GetValue(obj, null);

				buf.Append(property.Name + "=");
				_GetMessage(value, buf);
            
            }
            buf.Append("}");
        }

		#region Private Members
		
		/// <summary>
		/// 巡回するようなマッピングオブジェクトには現バージョンでは対応していません。
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="buf"></param>
		private  static void _GetMessage(object obj, StringBuilder buf)
		{
			if (obj == null) 
			{
				buf.Append("null");
			}
			else if (obj is ValueType) 
			{
				buf.Append(obj.ToString());
			}
			else if (obj is String) 
			{
				buf.Append("\"");
				buf.Append(obj.ToString());
				buf.Append("\"");
			}
			else if (obj is IDictionary)
			{
				IDictionaryEnumerator enumrator = ((IDictionary)obj).GetEnumerator();
				bool f = false;
				buf.Append("{");
				while(enumrator.MoveNext()) 
				{
					if (f)	buf.Append(",");
					else 	f = true;

					GetMessage(enumrator.Key, buf);
					buf.Append("=");
					GetMessage(enumrator.Value, buf);
				}
				buf.Append("}");
			}
			else if (obj is Array) 
			{
				Array ar = (Array)obj;
				bool f = false;
				buf.Append("{");
				foreach (object o in ar)
				{
					if (f)	buf.Append(",");
					else 	f = true;

					GetMessage(o, buf);
				}
				buf.Append("}");
			}
			/*
			else if (obj is ArrayList) 
			{
				ArrayList al = (ArrayList)obj;
				bool f = false;
				buf.Append("{");
				foreach (object o in al)
				{
					if (f)	buf.Append(",");
					else 	f = true;

					GetMessage(o, buf);
				}
				buf.Append("}");
			}
			*/
			else if (obj is DataSet)
			{
				_GetMessageDataSet((DataSet)obj, buf);
			}
			/*
			else if (obj is DataTable) 
			{
			}
			*/
			else if (obj is Exception)
			{
				//bufに例外のエラーを追加します。
				buf.Append(GetExceptionMessageForDeveloper((Exception)obj));
			}
				//List<string>とList<int>を処理
			else if (obj is IList)
			{
				IList list = (IList)obj;
				bool f = false;
				buf.Append("{");
				foreach (object o in list)
				{
					if (f) buf.Append(",");
					else f = true;

					GetMessage(o, buf);
				}
				buf.Append("}");
			}
			else if (obj is IProperty)
			{
				buf.Append("[");
				buf.Append(obj.GetType().Name);
				buf.Append("]");
				GetPropertyMessage(obj, buf, null);
			}
			else
			{
				buf.Append("[");
				buf.Append(obj.GetType().Name);
				buf.Append("]");
				buf.Append(obj.ToString());
			}
		}

		private static void _GetMessageDataSet(DataSet dataSet, StringBuilder buf)
		{
			//abekiyonow
			//デバックのレベルにより、ここで全てのデータをログに出力する。

//			System.IO.MemoryStream memStream = new System.IO.MemoryStream();
//			dataSet.WriteXml(memStream, System.Data.XmlWriteMode.IgnoreSchema);
//			buf.Append(System.Text.Encoding.Default.GetString(memStream.ToArray()));
//			buf.Append(System.Environment.NewLine);

			//IntelliSence Output		
			bool f = false;
			buf.Append("[");
			buf.Append(dataSet.GetType().Name);
			buf.Append("]");
			buf.Append("{");
			foreach(DataTable d in dataSet.Tables) 
			{
				if (f)	buf.Append(",");
				else 	f = true;
				buf.Append(d.TableName);
				buf.Append("(");
				buf.Append(d.Rows.Count);
				buf.Append(",");
				buf.Append(d.Columns.Count);
				buf.Append(")");				
			}
			buf.Append("}");
		}

		static string _GetExceptipnMessage(Exception ex)
		{
			if (ex == null)
			{
				return "";
			}
			string ret = ex.Message;

			if (ex.InnerException != null)
			{
				ret += Environment.NewLine;
				ret += _GetExceptipnMessage(ex.InnerException);
			}

			return ret;
		}
		#endregion


	}
}
