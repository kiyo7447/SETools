using System;
using System.Collections.Generic;
using System.Text;

namespace ALib
{
	public abstract class AbstractMessage
	{

		public virtual AbstractMessageNormal Normal
		{
			get
			{
				throw new SystemException("プロパティが実装されていません。AbstractMessage.Normal");
			}
		}

		public virtual AbstractMessageError Error
		{
			get
			{
				throw new SystemException("プロパティが実装されていません。AbstractMessage.Normal");
			}
		}

	}
}
