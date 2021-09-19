using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace ALauncher
{
	public class SettingInfo
	{
		//private Dictionary<int, ApplicationModel> _applications = new Dictionary<int,ApplicationModel>();

		//public Dictionary<int, ApplicationModel> Applications
		//{
		//    get { return _applications; }
		//    set { _applications = value; }
		//}


		//private List<ApplicationModel> _applications = new List<ApplicationModel>();

		//public List<ApplicationModel> Applications
		//{
		//    get { return _applications; }
		//    set { _applications = value; }
		//}

		private List<ApplicationModel> _applications = new List<ApplicationModel>();

		//[XmlArray("出力設定フィールド")]
		//[XmlArrayItem(typeof(ApplicationModel), ElementName = "フィールド")]
		[XmlArrayItem(typeof(ApplicationModel))]
		public List<ApplicationModel> Applications
		{
			get { return _applications; }
			set { _applications = value; }
		}

		private string _test;

		public string Test
		{
			get { return _test; }
			set { _test = value; }
		}


		public void Serialize(FileInfo fileInfo)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(SettingInfo));
			using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Create))
			{
				serializer.Serialize(stream, this);
			}
		}

		public static SettingInfo Deserialize(FileInfo fileInfo)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(SettingInfo));
			SettingInfo settingInfo;
			using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
			{
				settingInfo = (SettingInfo)serializer.Deserialize(stream);
			}
			return settingInfo;
		}
	}
}
