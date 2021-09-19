using System;
using System.Collections.Generic;
using System.Text;

namespace ALib
{
	public class AbstractMessageNormal
	{
		public virtual void Show(string msg)
		{
			throw new SystemException("メソッドが実装されていません。");
		}

	}
}
