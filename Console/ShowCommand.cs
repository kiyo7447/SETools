using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace SConsole
{
	class ShowCommand
	{
		TextWriter _textWriter = null;
		public ShowCommand(TextWriter w)
		{
			_textWriter = w;
		}
		public int Show(string key)
		{
			Type con = typeof(Console);

			PropertyInfo pInfo = con.GetProperty(key);
			if (pInfo == null)
			{
				//Propertyなし
				_textWriter.WriteLine("プロパティが見つかりません。大文字小文字は区別されます。(" + key + "）");

				return 1;
			}

			object p = con.InvokeMember(key, BindingFlags.GetProperty, null, null, null);

			if (p != null)
			{
				string vString = GetString(p);
				if (vString != null)
				{
					_textWriter.WriteLine(vString);
					return 0;
				}
			}
			return 1;
		}

		public int ShowAll()
		{
			Type con = typeof(Console);
			PropertyInfo[] props = con.GetProperties();
			//StringBuilder sb = new StringBuilder(1024);
			foreach (PropertyInfo pro in props)
			{
				object v = pro.GetValue(null, null);

				string vString = GetString(v);

				if (vString != null)
				{
					//sb.AppendLine(pro.Name + "=" + vString);
					_textWriter.WriteLine(pro.Name + "=" + vString);
				}
			}
			//_textWriter.WriteLine(sb.ToString());
			return 0;
		}
		string GetString(object o)
		{
			if (o.GetType().IsEnum == true)
			{
				//enum
				return o.ToString();
			}

			if (o.GetType().IsPrimitive == true)
			{
				//primitive
				return o.ToString();
			}

			if (o is Encoding)
			{
				//encoding
				Encoding e = (Encoding)o;
				return e.WebName + "(" + e.WindowsCodePage + ")";
			}

			if (o.GetType().FullName == "System.String")
			{
				return o.ToString();
			}
			return null;
		}
	}
}
