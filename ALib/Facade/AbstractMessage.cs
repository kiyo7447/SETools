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
				throw new SystemException("�v���p�e�B����������Ă��܂���BAbstractMessage.Normal");
			}
		}

		public virtual AbstractMessageError Error
		{
			get
			{
				throw new SystemException("�v���p�e�B����������Ă��܂���BAbstractMessage.Normal");
			}
		}

	}
}
