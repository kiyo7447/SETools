using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ALib
{
	/// <summary>
	/// WakeOnLan�֘A��Facade�N���X
	/// </summary>
	public class WakeOnLan
	{
		/// <summary>
		/// WakeUpLan�p�̃N���X
		/// </summary>
		/// <param name="macAddress"></param>
		/// <returns>byteSend</returns>
		public int WakeUp(ref string macAddress)
		{
			//create MagicPacket(TM)
			MagicPacket wakeUpPacket = new MagicPacket(macAddress);

			//wake up machine
			int byteSend = wakeUpPacket.WakeUp();
			macAddress = wakeUpPacket.MacAddress;
			return byteSend;
		}
	}
}
