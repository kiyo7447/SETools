using System;
using System.Collections.Generic;
using System.Text;

namespace Cal
{
	class Disp
	{
		DateTime _dt = DateTime.MinValue;
		Holidays _holi = null;
		ColorManager _colorManager = new ColorManager();

		public Disp(DateTime yearMonth, Holidays holidays)
		{
			_dt = yearMonth;
			_holi = holidays;
		}

		public DateTime YearMonth
		{
			set
			{
				_dt = value;
			}
		}

		public void Write(int line)
		{
			_Write(line, false);
		}

		public void WriteLine(int line)
		{
			this._Write(line, true);
		}

		void _Write(int line, bool newLine)
		{
			if (line == 0)
			{
				Console.Out.Write("      ");
				Console.Out.Write(_dt.Year.ToString("0000"));
				Console.Out.Write(" ");
				Console.Out.Write(_dt.Month.ToString("00"));
				Console.Out.Write("       ");
			}
			if (line == 1)
			{
				ConsoleColor foregroundColor = Console.ForegroundColor;
				Console.Out.Write("Mo Tu We Th Fr ");
				Console.ForegroundColor = _colorManager.SaturdayForegroundColor;
				Console.Out.Write("Sa ");
				Console.ForegroundColor = _colorManager.SundayForegroundColor;
				Console.Out.Write("Su");
				Console.ForegroundColor = foregroundColor;
			}
			DateTime dt = _dt.AddDays(-1 * _dt.Day + 1);
			//sun 0
			//mon 1
			//･･･
			//sat 6
			int week = (int)dt.DayOfWeek - 1;
			if (week == -1) week = 6;		//日曜日は0から7へ変更
			if (line == 2)
			{
				for (int cnt = 0; cnt < week; cnt++)
				{
					Console.Out.Write("   ");
				}
				do
				{
					_WriteDay(dt, true);
					dt = dt.AddDays(1);
				}
				while (dt.DayOfWeek != DayOfWeek.Monday);
			}
			if (line > 2)
			{
				//行のからまわし
				for (int c = line; c > 2; c--)
				{
					do
					{
						dt = dt.AddDays(1);
					}
					while (dt.DayOfWeek != DayOfWeek.Monday);
				}
				//行の出力
				do
				{
					if (_dt.Year == dt.Year && _dt.Month == dt.Month)
					{
						_WriteDay(dt, true);
					}
					else
					{
						_WriteDay(dt, false);
					}
					dt = dt.AddDays(1);
				}
				while (dt.DayOfWeek != DayOfWeek.Monday);
			}

			if (newLine == true)
			{
				Console.Out.WriteLine();
			}

		}

		void _WriteDay(DateTime dt, bool isDisp)
		{
			if (isDisp == true)
			{
				ConsoleColor foregroundColor = Console.ForegroundColor;
				ConsoleColor backgrondColor = ConsoleColor.Black;
				DateTime n = DateTime.Now;
				if (dt.Year == n.Year && dt.Month == n.Month && dt.Day == n.Day)
				{
					backgrondColor = Console.BackgroundColor;
					Console.BackgroundColor = _colorManager.TodayBackgroundColor;
				}

				//if (dt.Month == 4 && dt.Day == 29)
				//{
				//    Debug.Print("ok");
				//}

                if (_holi.IsHoliday(dt) == true)
                {
                    Console.ForegroundColor = _colorManager.HolidayForegroundColor;
                    Console.Out.Write(String.Format("{0, 2}", dt.Day));
                    Console.ForegroundColor = foregroundColor;
                }
                else if (dt.DayOfWeek == DayOfWeek.Saturday)
				{
					Console.ForegroundColor = _colorManager.SaturdayForegroundColor;
					Console.Out.Write(String.Format("{0, 2}", dt.Day));
					Console.ForegroundColor = foregroundColor;
				}
				else if (dt.DayOfWeek == DayOfWeek.Sunday)
				{
					Console.ForegroundColor = _colorManager.SundayForegroundColor;
					Console.Out.Write(String.Format("{0, 2}", dt.Day));
					Console.ForegroundColor = foregroundColor;

				}
				else
				{
					Console.Out.Write(String.Format("{0, 2}", dt.Day));
				}

				if (dt.Year == n.Year && dt.Month == n.Month && dt.Day == n.Day)
				{
					Console.BackgroundColor = backgrondColor;
				}

				if (dt.DayOfWeek != DayOfWeek.Sunday)
				{
					Console.Out.Write(" ");
				}
			}
			else
			{
				if (dt.DayOfWeek == DayOfWeek.Sunday)
				{
					Console.Out.Write("  ");
				}
				else
				{
					Console.Out.Write("   ");

				}
			}
		}
	}
}
