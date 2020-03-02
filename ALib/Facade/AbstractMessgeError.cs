using System;
using System.Collections.Generic;
using System.Text;

namespace ALib
{
	public class AbstractMessageError
	{
		public virtual void Show(Exception ex)
		{
			throw new SystemException("メソッドが実装されていません。AbstractMessageError.Show");
		}
		public virtual void Show(string msg)
		{
			throw new SystemException("メソッドが実装されていません。AbstractMessageError.Show");
		}
	}
}
