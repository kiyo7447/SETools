using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SConsole
{
	class BeepCommand
	{
		Dictionary<string, int> _melody = null;

		public BeepCommand()
		{
			if (_melody == null)
			{
				_melody = new Dictionary<string, int>(30);
			}
			_melody.Add("ラ-", 220);
			_melody.Add("ラ-＃", 233);
			_melody.Add("シ-", 247);
			_melody.Add("ド", 262);
			_melody.Add("ド＃", 277);
			_melody.Add("レ", 294);
			_melody.Add("ミ", 330);
			_melody.Add("ファ", 349);
			_melody.Add("ファ＃", 370);
			_melody.Add("ソ", 392);
			_melody.Add("ソ＃", 415);
			_melody.Add("ラ", 440);
			_melody.Add("ラ＃", 466);
			_melody.Add("シ", 494);
			_melody.Add("ド+", 523);
			_melody.Add("ド+＃", 554);
			_melody.Add("レ+", 587);
			_melody.Add("レ+＃", 622);
			_melody.Add("ミ+", 659);
			_melody.Add("ファ+", 699);
			_melody.Add("ファ+＃", 740);
			_melody.Add("ソ+", 784);
			_melody.Add("ソ+＃", 830);
			_melody.Add("ラ+", 880);
			_melody.Add("ラ+＃", 932);
			_melody.Add("シ+", 988);
			_melody.Add("ド++", 1047);

		}

		public void Beep(string kyoku)
		{
			if (kyoku.ToUpper() == "FF")
			{
				//final fantasy battle end
				//ドドドドーソ♯ーラ＃－ドーラ＃ドー 
				_Beep("ド++", 100);
				_Sleep(5);
				_Beep("ド++", 100);
				_Sleep(5);
				_Beep("ド++", 100);
				_Sleep(2);
				_Beep("ド++", 500);
				_Sleep(2);
				_Beep("ソ+＃", 500);
				_Sleep(2);
				_Beep("ラ+＃", 500);
				_Sleep(2);
				_Beep("ド++", 400);
				_Sleep(2);
				_Beep("ラ+＃", 100);
				_Beep("ド++", 700);
				return;
			}
			if (kyoku.ToUpper() == "MARIO")
			{
				//mario 1up
				//ミ、ソ、ミ、ド、レ、ソ 
				//低　低　高　高　高　高 
				//_Sleep(500);
				for (int i = 0; i < 1; i++)
				{
					_Beep("ミ", 50);
					_Beep("ソ", 50);
					_Beep("ミ+", 50);
					_Beep("ド+", 50);
					_Beep("レ+", 50);
					_Beep("ソ+", 50);
					_Sleep(200);
				}

				return;

			}
			if (kyoku.ToUpper() == "DQ")
			{
				//dragon quest level up
				//ファ　ファ　ファ　♭ミ　ソ　ファー 
				//_Beep("ファ+", 120);
				_Beep(1010, 150);
				_Sleep(5);
				//_Beep("ファ+", 120);
				_Beep(1010, 150);
				_Sleep(5);
				//_Beep("ファ+", 150);
				_Beep(1010, 150);
				_Sleep(0);
				//_Beep("レ+＃", 160);
				_Beep(915, 150);
				_Sleep(120);
				//_Beep("ソ+", 120);
				_Beep(1150,150);
				_Sleep(120);
				//_Beep("ファ+", 800);
				_Beep(1010, 700);
				return;

			}
			int a, b, c;
			if (kyoku.ToUpper() == "KAERU")
			{
				if (true)
				{
					//song of frog
					//ドレミファミレド　　ミファソラソファミ
					//ド　ド　ド　ド　　　ドドレレミミファファミ　レ　ド
					_Beep("ド", 400);
					_Beep("レ", 400);
					_Beep("ミ", 400);
					_Beep("ファ", 400);
					_Beep("ミ", 400);
					_Beep("レ", 400);
					_Beep("ド", 400);
					_Sleep(300);

					_Beep("ミ", 400);
					_Beep("ファ", 400);
					_Beep("ソ", 400);
					_Beep("ラ", 400);
					_Beep("ソ", 400);
					_Beep("ファ", 400);
					_Beep("ミ", 400);
					_Sleep(300);

					a = 600;
					b = 200;
					_Beep("ド", a);
					_Sleep(b);

					_Beep("ド", a);
					_Sleep(b);

					_Beep("ド", a);
					_Sleep(b);

					_Beep("ド", a);
					_Sleep(b);
				}
				a = 200;
				b = 80;
				c = 120;
				_Beep("ド", a);
				_Sleep(b);
				_Beep("ド", a);
				_Sleep(c);
				_Beep("レ", a);
				_Sleep(b);
				_Beep("レ", a);
				_Sleep(c);
				_Beep("ミ", a);
				_Sleep(b);
				_Beep("ミ", a);
				_Sleep(c);
				_Beep("ファ", a);
				_Sleep(b);
				_Beep("ファ", a);
				_Sleep(c);

				b = 100;
				_Beep("ミ", 500);
				_Sleep(100);

				_Beep("レ", 600);
				_Sleep(100);

				_Beep("ド", 800);
				_Sleep(100);
				return;

			}

			_Beep(kyoku, 100);
		}

		public void Beep(int frequency, int duration)
		{
			Console.Beep(frequency, duration);
		}

		void _Sleep(int s)
		{
			Thread.Sleep(s);
		}
		void _Beep(int i, int t)
		{
			Console.Beep(i, t);
		}
		void _Beep(string s, int t)
		{
			int f = _melody[s];
			Console.Beep(f, t);
		}

	}
}
