using System;
using System.Collections.Generic;
using System.Text;

namespace Cal
{
	public class Holiday
	{
		int _minYear = 0;
		int _maxYear = 9999;
		int _month = 0;
		int _day = 0;
		string _name;
		public Holiday(string line)
		{
			if (string.IsNullOrEmpty(line))
			{
				return;
			}
			string[] fields = line.Split(new char[] { ',' });

			_minYear = int.Parse(fields[0]);
			_maxYear = int.Parse(fields[1]);
			_month = int.Parse(fields[2]);
			_day = int.Parse(fields[3]);
			_name = fields[4];
		}

		public int MinYear
		{
			get
			{
				return _minYear;
			}
		}
		public int MaxYear
		{
			get
			{
				return _maxYear;
			}
		}
		public int Month
		{
			get
			{
				return _month;
			}
		}
		public int Day
		{
			get
			{
				return _day;
			}
		}
		public string Name
		{
			get
			{
				return _name;
			}
		}
	}
}
