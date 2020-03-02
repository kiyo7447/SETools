using System;
using System.Collections.Generic;
using System.Text;

namespace Cal
{
	class Parameter : AbstractParameter
	{
		bool _isHelp = false;

		bool _isYearOnly = false;

		DateTime _yearMonth = DateTime.MinValue;


		public Parameter(string[] args)
			: base(args)
		{
			_SetParameter(args);
		}

		void _SetParameter(string[] args)
		{

			string[] values;

			values = base.GetParameter("D", 0);
			if (values != null)
			{
				Context.IsDebug  = true;
			}

			values = base.GetParameter("?", 0);
			if (values != null)
			{
				_isHelp = true;
			}

			values = base.GetParameter("help", 0);
			if (values != null)
			{
				_isHelp = true;
			}


			//Custom
			values = base.GetParameter(1, 0);
			if (values != null)
			{
				if (values.Length == 0)
				{
					_yearMonth = DateTime.Now;
				}
				else
				{
					try
					{
						if (values[0].Trim().ToUpper() == "Y")
						{
							//今年
							_yearMonth = new DateTime(DateTime.Now.Year, 1, 1);
							_isYearOnly = true;
						}
						else
						{
							int ym = int.Parse(values[0]);

							if (ym >= 1 && ym <= 12)
							{
								_yearMonth = new DateTime(DateTime.Now.Year, ym, 1);
								_isYearOnly = false;
							}
							else if (ym >= 13 && ym <= 9999)
							{
								_yearMonth = new DateTime(ym, 1, 1);
								_isYearOnly = true;
							}
							else
							{
								_yearMonth = new DateTime(ym / 100, ym % 100, 1);
								_isYearOnly = false;
							}
						}
					}
					catch (Exception ex)
					{
						throw new SystemException("パラメータ(年月)は数字でなければなりません。value=" + values[0], ex);
					}
				}
			}
		}


		public bool IsDebug
		{
			get
			{
				return Context.IsDebug;
			}
			set
			{
				Context.IsDebug = value;
			}
		}
		public bool IsHelp
		{
			get
			{
				return _isHelp;
			}
			set
			{
				_isHelp = value;
			}
		}


		public DateTime YearMonth
		{
			get
			{
				return _yearMonth;
			}

			set
			{
					_yearMonth = value;
			}
		}

		public bool IsYearOnly
		{
			get
			{
				return _isYearOnly;
			}
		}

		public new String ToString()
		{
			return Dump.GetMessage(this);
		}
	}
}
