using System;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

namespace ALib
{
	/// <summary>
	/// MagicPacketに関するクラス
	/// </summary>
	public class MagicPacket
	{
		private const int HEADER = 6;
		private const int BYTELENGHT = 6;
		private const int MAGICPACKETLENGTH = 16;

		private System.Net.IPAddress wolIPAddr = System.Net.IPAddress.Broadcast;
		private int wolPortAddr = 7;
		private IPEndPoint wolEndPoint;				
		private byte[] wolMacAddr;
		private byte[] magicPacketPayload;
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="macAddress"></param>
		public MagicPacket(string macAddress)
		{		
			wolMacAddr = Mac2Byte(macAddress);
			magicPacketPayload = CreatePayload(wolMacAddr);
			wolEndPoint = new System.Net.IPEndPoint(wolIPAddr, wolPortAddr);
  		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="macAddress"></param>
		/// <param name="strPortAddress"></param>
		public MagicPacket(string macAddress, string strPortAddress)
		{		
			wolMacAddr = Mac2Byte(macAddress);
			magicPacketPayload = CreatePayload(wolMacAddr);
			wolPortAddr = Convert.ToInt16(strPortAddress, 10);
			wolEndPoint = new System.Net.IPEndPoint(wolIPAddr, wolPortAddr);
		}
		
		/// <summary>
		/// MACAddress
		/// </summary>
		public string MacAddress 
		{
			get 
			{
				string strMacAdress = "";
				for (int i=0; i<wolMacAddr.Length; i++)
				{
					strMacAdress += wolMacAddr[i].ToString("X2");
				}
				return strMacAdress;
			}
		}

		/// <summary>
		/// MacAddressをバイトの配列に変換します。
		/// </summary>
		/// <param name="strMacAddress"></param>
		/// <returns></returns>
		protected static byte[] Mac2Byte(string strMacAddress)
		{
			string macAddr;
			byte[] macBytes = new byte[BYTELENGHT];
			macAddr = Regex.Replace(strMacAddress, @"[^0-9A-Fa-f]", "");
			if (!(macAddr.Length == BYTELENGHT*2))
				throw new ArgumentException("Mac Adress must be "+ (BYTELENGHT*2).ToString() +" digits of 0-9, A-F, a-f characters in length.");
			string hex;
			for (int i=0; i<macBytes.Length;i++)
			{
				hex = new String(new Char[] {macAddr[i*2], macAddr[i*2+1]});
				macBytes[(i)] = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
			}
			return macBytes;
		}
		
		/// <summary>
		/// MagicPacketのペイロードを作成します。
		/// </summary>
		/// <param name="macAddress"></param>
		/// <returns></returns>
		protected static byte[] CreatePayload(byte[] macAddress) 
		{
			byte[] payloadData = new byte[HEADER+MAGICPACKETLENGTH*BYTELENGHT];
			for (int i=0; i<HEADER; i++) 
			{
				payloadData[i] = byte.Parse("FF", System.Globalization.NumberStyles.HexNumber);
			}
			for(int i=0; i<MAGICPACKETLENGTH; i++)
			{
				for(int j=0;j<BYTELENGHT;j++)
				{
					payloadData[((i*BYTELENGHT)+j)+HEADER] = macAddress[j];
				}
			}
			return payloadData;
		}
		
		/// <summary>
		/// WakeUpを実行する
		/// </summary>
		/// <returns></returns>
		public int WakeUp() {
			return SendUDP(magicPacketPayload, wolEndPoint);
		}
		
		/// <summary>
		/// UDPパケットを送る
		/// </summary>
		/// <param name="Payload"></param>
		/// <param name="EndPoint"></param>
		/// <returns></returns>
		protected static int SendUDP(byte[] Payload, IPEndPoint EndPoint)
		{		
			int byteSend;
			Socket socketClient = new Socket(EndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
			try
			{
				socketClient.Connect(EndPoint);
				byteSend = socketClient.Send (Payload, 0, Payload.Length, SocketFlags.None);
			}
			catch (SocketException ex)
			{
				throw ex;
			}
			finally
			{
				socketClient.Close();
			}
			return byteSend;
  		}
 	}
}
