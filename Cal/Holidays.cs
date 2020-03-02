using System;
using System.Collections.Generic;
using System.Text;

namespace Cal
{
	public class Holidays
	{
		List<Holiday> _holi = new List<Holiday>();
		public Holidays(string loadText)
		{
			if (loadText == null)
			{
				throw new SystemException($"パラメータがnullです。loadText={loadText}");
			}
			foreach (string line in loadText.Split(new char[] { '\n' }))
			{
				_holi.Add(new Holiday(line));
			}

		}

		public bool IsHoliday(DateTime dt)
		{
			foreach (Holiday holi in _holi)
			{
				if (holi.MinYear <= dt.Year && holi.MaxYear >= dt.Year)
				{
					if (dt.Month == holi.Month && dt.Day == holi.Day)
					{
						return true;
					}
				}
			}
			return false;
		}

	}
}
