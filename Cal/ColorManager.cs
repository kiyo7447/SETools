using System;
using System.Collections.Generic;
using System.Text;

namespace Cal
{
	class ColorManager
	{
		ConsoleColor _sundayForegroundColor = ConsoleColor.Red;
		ConsoleColor _saturdayForegroundColor = ConsoleColor.Blue;
		ConsoleColor _holidayForegroundColor = ConsoleColor.Red;
		ConsoleColor _todayBackgroundColor = ConsoleColor.DarkGray;

		public ColorManager()
		{
			Properties.Settings setting = new Properties.Settings();

			_sundayForegroundColor = (ConsoleColor)Enum.Parse(_sundayForegroundColor.GetType(), setting.SundayForegroundColor);
			_saturdayForegroundColor = (ConsoleColor)Enum.Parse(_saturdayForegroundColor.GetType(), setting.SaturdayForegroundColor);
			_holidayForegroundColor = (ConsoleColor)Enum.Parse(_holidayForegroundColor.GetType(), setting.HolidayForegroundColor);
			_todayBackgroundColor = (ConsoleColor)Enum.Parse(_todayBackgroundColor.GetType(), setting.ToDayBackgroundColor);
		}

		public ConsoleColor SundayForegroundColor
		{
			get
			{
				return _sundayForegroundColor;
			}
		}

		public ConsoleColor SaturdayForegroundColor
		{
			get
			{
				return _saturdayForegroundColor;
			}
		}


		public ConsoleColor HolidayForegroundColor
		{
			get
			{
				return _holidayForegroundColor;
			}
		}

		public ConsoleColor TodayBackgroundColor
		{
			get
			{
				return _todayBackgroundColor;
			}
		}

	}
}
