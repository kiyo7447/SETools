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
		 *  Cal.exe y		���N��S���\��
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
			//���ƌ��̊�	��
			const string separatorY = "�b";
			//���ƌ��̊�	�c
			//����PG�ł͏c�̃Z�p���[�^�͐ݒ肵�Ă��Ȃ��B
			//�Ȃ�ׂ����𑽂����Ă���B�Ђƌ��̍ő�s���́A6�s���B
			/* Ex)
			 *       2010 08
			 * Mo Tu We Th Fr Sa Su
			 *                    1		�P�s��
			 *  2  3  4  5  6  7  8		�Q�s��
			 *  9 10 11 12 13 14 15		�R�s��
			 * 16 17 18 19 20 21 22		�S�s��
			 * 23 24 25 26 27 28 29		�T�s��
			 * 30 31					�U�s��
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
						//1�N���o��
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
						//�O�������o��

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
					throw new SystemException("�p�����[�^�̉�͂̎��s���܂����Bargs=" + Dump.GetMessage(args), exPara);
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
