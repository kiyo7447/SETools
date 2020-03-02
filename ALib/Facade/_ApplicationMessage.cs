using System;
using System.Collections.Generic;
using System.Text;

namespace ALib
{
	public class ApplicationMessage : AbstractMessage
	{
		ApplicationMessageError _error = null;
		ApplicationMessageNormal _normal = null;


		public override AbstractMessageError Error
		{
			get
			{
				if (_error == null)
				{
					_error = new ApplicationMessageError();
				}
				return _error;
			}
		}

		public override AbstractMessageNormal Normal
		{
			get
			{
				if (_normal == null)
				{
					_normal = new ApplicationMessageNormal();
				}
				return _normal;
			}
		}
	}
}
