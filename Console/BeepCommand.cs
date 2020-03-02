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
			_melody.Add("��-", 220);
			_melody.Add("��-��", 233);
			_melody.Add("�V-", 247);
			_melody.Add("�h", 262);
			_melody.Add("�h��", 277);
			_melody.Add("��", 294);
			_melody.Add("�~", 330);
			_melody.Add("�t�@", 349);
			_melody.Add("�t�@��", 370);
			_melody.Add("�\", 392);
			_melody.Add("�\��", 415);
			_melody.Add("��", 440);
			_melody.Add("����", 466);
			_melody.Add("�V", 494);
			_melody.Add("�h+", 523);
			_melody.Add("�h+��", 554);
			_melody.Add("��+", 587);
			_melody.Add("��+��", 622);
			_melody.Add("�~+", 659);
			_melody.Add("�t�@+", 699);
			_melody.Add("�t�@+��", 740);
			_melody.Add("�\+", 784);
			_melody.Add("�\+��", 830);
			_melody.Add("��+", 880);
			_melody.Add("��+��", 932);
			_melody.Add("�V+", 988);
			_melody.Add("�h++", 1047);

		}

		public void Beep(string kyoku)
		{
			if (kyoku.ToUpper() == "FF")
			{
				//final fantasy battle end
				//�h�h�h�h�[�\��[�����|�h�[�����h�[ 
				_Beep("�h++", 100);
				_Sleep(5);
				_Beep("�h++", 100);
				_Sleep(5);
				_Beep("�h++", 100);
				_Sleep(2);
				_Beep("�h++", 500);
				_Sleep(2);
				_Beep("�\+��", 500);
				_Sleep(2);
				_Beep("��+��", 500);
				_Sleep(2);
				_Beep("�h++", 400);
				_Sleep(2);
				_Beep("��+��", 100);
				_Beep("�h++", 700);
				return;
			}
			if (kyoku.ToUpper() == "MARIO")
			{
				//mario 1up
				//�~�A�\�A�~�A�h�A���A�\ 
				//��@��@���@���@���@�� 
				//_Sleep(500);
				for (int i = 0; i < 1; i++)
				{
					_Beep("�~", 50);
					_Beep("�\", 50);
					_Beep("�~+", 50);
					_Beep("�h+", 50);
					_Beep("��+", 50);
					_Beep("�\+", 50);
					_Sleep(200);
				}

				return;

			}
			if (kyoku.ToUpper() == "DQ")
			{
				//dragon quest level up
				//�t�@�@�t�@�@�t�@�@��~�@�\�@�t�@�[ 
				//_Beep("�t�@+", 120);
				_Beep(1010, 150);
				_Sleep(5);
				//_Beep("�t�@+", 120);
				_Beep(1010, 150);
				_Sleep(5);
				//_Beep("�t�@+", 150);
				_Beep(1010, 150);
				_Sleep(0);
				//_Beep("��+��", 160);
				_Beep(915, 150);
				_Sleep(120);
				//_Beep("�\+", 120);
				_Beep(1150,150);
				_Sleep(120);
				//_Beep("�t�@+", 800);
				_Beep(1010, 700);
				return;

			}
			int a, b, c;
			if (kyoku.ToUpper() == "KAERU")
			{
				if (true)
				{
					//song of frog
					//�h���~�t�@�~���h�@�@�~�t�@�\���\�t�@�~
					//�h�@�h�@�h�@�h�@�@�@�h�h�����~�~�t�@�t�@�~�@���@�h
					_Beep("�h", 400);
					_Beep("��", 400);
					_Beep("�~", 400);
					_Beep("�t�@", 400);
					_Beep("�~", 400);
					_Beep("��", 400);
					_Beep("�h", 400);
					_Sleep(300);

					_Beep("�~", 400);
					_Beep("�t�@", 400);
					_Beep("�\", 400);
					_Beep("��", 400);
					_Beep("�\", 400);
					_Beep("�t�@", 400);
					_Beep("�~", 400);
					_Sleep(300);

					a = 600;
					b = 200;
					_Beep("�h", a);
					_Sleep(b);

					_Beep("�h", a);
					_Sleep(b);

					_Beep("�h", a);
					_Sleep(b);

					_Beep("�h", a);
					_Sleep(b);
				}
				a = 200;
				b = 80;
				c = 120;
				_Beep("�h", a);
				_Sleep(b);
				_Beep("�h", a);
				_Sleep(c);
				_Beep("��", a);
				_Sleep(b);
				_Beep("��", a);
				_Sleep(c);
				_Beep("�~", a);
				_Sleep(b);
				_Beep("�~", a);
				_Sleep(c);
				_Beep("�t�@", a);
				_Sleep(b);
				_Beep("�t�@", a);
				_Sleep(c);

				b = 100;
				_Beep("�~", 500);
				_Sleep(100);

				_Beep("��", 600);
				_Sleep(100);

				_Beep("�h", 800);
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
