using System;
using System.Collections.Generic;
using System.Text;

namespace ALib
{
	public class ApplicationMessageError : AbstractMessageError
	{

		public override void Show(string msg)
		{
			base.Show(msg);
		}

		public override void Show(Exception ex)
		{
			base.Show(ex);
		}

	}
}
