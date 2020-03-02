using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ALib
{
	/// <summary>
	/// TODO Windowî≈Ç‚ASPî≈ÅASilverlightî≈ÅAWPFî≈Ç…ëŒâû
	/// </summary>
	public class ConsoleMessage : AbstractMessage
	{
		AbstractMessageError _error = null;
		AbstractMessageNormal _normal = null;

		public override AbstractMessageError Error
		{
			get
			{
				if (_error == null)
				{
					_error = new ConsoleMessageError();
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
					_normal = new ConsoleMessageNormal();
				}
				return _normal;
			}
		}

	//    GuidanceException _guidanceException = null;

	//    public GuidanceException Exception
	//    {
	//        get
	//        {
	//            if (_guidanceException == null)
	//            {
	//                _guidanceException = new GuidanceException();
	//            }
	//            return _guidanceException;
	//        }
	//    }

	}

	//public class GuidanceException
	//{
	//    public void Show(Exception ex)
	//    {
	//        //TODO
	//        Console.Out.WriteLine(ex.ToString());
	//    }
	//}
}
