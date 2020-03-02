using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Cal
{
	class Program
	{
		/*
		 *	Cal.exe 201005
		 *  Cal.exe 2010
		 *  Cal.exe
		 *  Cal.exe y		今年を全部表示
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * 
		 * */
		static int Main(string[] args)
		{
			//月と月の間	横
			const string separatorY = "｜";
			//月と月の間	縦
			//このPGでは縦のセパレータは設定していない。
			//なるべく情報を多くしている。ひと月の最大行数は、6行だ。
			/* Ex)
			 *       2010 08
			 * Mo Tu We Th Fr Sa Su
			 *                    1		１行目
			 *  2  3  4  5  6  7  8		２行目
			 *  9 10 11 12 13 14 15		３行目
			 * 16 17 18 19 20 21 22		４行目
			 * 23 24 25 26 27 28 29		５行目
			 * 30 31					６行目
			 */
			//const string separatorX = "-";
			Parameter parameter;

			try
			{
				try
				{
					parameter = new Parameter(args);

					Properties.Settings setting = new Properties.Settings();
					string holiday = setting.Holiday;
					Holidays holi = new Holidays(holiday);

					if (parameter.IsYearOnly == true)
					{
						List<Disp> disps = new List<Disp>();
						//1年分出力
						for (int cnt = 1;cnt <= 12;cnt++)
						{
							disps.Add(new Disp(new DateTime(parameter.YearMonth.Year, cnt, 1), holi));
						}

						for (int row = 0; row < 4; row++)
						{
							for (int c = 0; c < 8; c++)
							{
								for (int col = 0; col < 3; col++)
								{
									disps[row * 3 + col].Write(c);
									Console.Out.Write(separatorY);
								}
								Console.Out.WriteLine();
							}
						}
					}
					else
					{
						//三か月分出力

						DateTime dt1 = parameter.YearMonth.AddMonths(-1);
						DateTime dt2 = parameter.YearMonth;
						DateTime dt3 = parameter.YearMonth.AddMonths(1);

						Disp d1 = new Disp(dt1, holi);
						Disp d2 = new Disp(dt2, holi);
						Disp d3 = new Disp(dt3, holi);

						for (int c = 0; c < 8; c++)
						{
							d1.Write(c);
							Console.Out.Write(separatorY);
							d2.Write(c);
							Console.Out.Write(separatorY);
							d3.Write(c);
							Console.Out.WriteLine();
						}
					}
				}
				catch (Exception exPara)
				{
					throw new SystemException("パラメータの解析の失敗しました。args=" + Dump.GetMessage(args), exPara);
				}
				return 0;
			}
			catch (Exception ex)
			{
				Facade.Message.Error.Show(ex);
				return 1;
			}
		}
	}
}
