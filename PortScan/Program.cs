using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace PortScan
{
	/// <summary>
	/// portscanに数秒が待てない人向け。21秒って長いよね
	/// </summary>
	class Program
	{
		static Dictionary<int, PortScanResult> _portScanResult = new Dictionary<int, PortScanResult>();

		static int Main(string[] args)
		{

			Parameter parameter = new Parameter(args);
			parameter.Port.All(c =>{_portScanResult.Add(c, new PortScanResult { Host = parameter.Host, Port = c}); return true;});
			_portScanResult.All(dic => { ThreadPool.QueueUserWorkItem(new WaitCallback(Scan), dic); return true; });

			Console.WriteLine($"ポートスキャンを開始します4。{_portScanResult.Min(c => c.Value.Port)}-{_portScanResult.Max(c => c.Value.Port)}");


			//WorkerThread Default 25
			//PortThread Default 1000
			ThreadPool.SetMaxThreads(100,5000);

//			ThreadPool.

			_portScanResult.All(dic =>
			{
				dic.Value.AutoResetEvent.WaitOne();
				if (dic.Value.ResultMessage.IndexOf("開いています") >= 0)
					Console.WriteLine(dic.Value.ResultMessage);
				return true;
			});

			//Console.ReadKey();
			return 0;
#if false
			TcpClient tc = new TcpClient(AddressFamily.InterNetwork);
			try
			{
				if (args.Length != 2)
				{
					throw new SystemException("パラメータ数に間違いがあります。パラメータ１＝ホスト名もしくは、IPアドレス、パラメータ２＝ポート番号");
				}

				var result = tc.BeginConnect(args[0], int.Parse(args[1]), null, null);
				//最大待ち時間は、21秒です。
				var sucess = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));

				//TcpClientは２１秒固定でタイム・アウトします。
				//その場合は、sucessとなりますが、tc.Connectedがfalseとなります。
				if (sucess && tc.Connected)
				{
					Console.Out.WriteLine("コネクションが開いています。");

				}
				else
				{
					Console.Out.WriteLine("コネクションがひらいいません。タイムアウトが発生しました。");

				}
				tc.Close();
				return 0;
			}
			catch (SocketException ex)
			{
				Console.Out.WriteLine(ex.ToString());
				return -1;
			}
			catch (Exception e)
			{
				Console.Out.Write(e.ToString());
				return -2;
			}
#endif
		}
		static void Scan(object state)
		{
			var keyValuePair = (KeyValuePair<int, PortScanResult>)state;
			var portScanResult = keyValuePair.Value;
			var result = portScanResult.TcpClient.BeginConnect(portScanResult.Host, portScanResult.Port, null, null);

			//最大待ち時間は、21秒です。
			var sucess = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

			//TcpClientは２１秒固定でタイム・アウトします。
			//その場合は、sucessとなりますが、tc.Connectedがfalseとなります。
			if (sucess && portScanResult.TcpClient.Connected)
				portScanResult.ResultMessage = $"Port={portScanResult.Port} ポートが開いています。";						
			else
				portScanResult.ResultMessage = $"Port={portScanResult.Port} ポートが開いていません。タイムアウトが発生しました。";
			portScanResult.TcpClient.Close();
			portScanResult.AutoResetEvent.Set();
		}
	}


	class PortScanResult
	{
		public TcpClient TcpClient { get; set; } = new TcpClient(AddressFamily.InterNetwork);
		public string Host { get; set; }
		public int Port { get; set; }
		public AutoResetEvent AutoResetEvent { get; set; } = new AutoResetEvent(false);
		public string ResultMessage { get; set; } = "";
	}

	class Parameter : AbstractParameter
	{
		public Parameter(string[] args) : base(args)
		{
			string[] values;
			Context.IsDebug = GetParameter("D", 0) != null ? true : false;
			IsHelp = GetParameter("help", 0) != null?true:false;


			//パラメータは、最低１個、最大で二個
			values = base.GetParameter(2, 1);
			Host = values[0];

			if (values.Count() == 2)
			{
				var ports = values[1].Split(new char[] { '-' });
				if (ports.Length > 1)
					//指定の開始から指定の終了前
					Enumerable.Range(int.Parse(ports[0]), int.Parse(ports[1])).All(c => { Port.Add(c); return true; });
				else
					//固定のIP
					Port.Add(int.Parse(ports[0]));
			}
			else
				//1-65535フルスキャン
				Enumerable.Range(1, 65535).All(c => { Port.Add(c); return true; });
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
		public bool IsHelp { get; set; }

		public string Host { get; set; }

		public List<int> Port { get; set; } = new List<int>();

		public new String ToString()
		{
			return Dump.GetMessage(this);
		}
	}
}

