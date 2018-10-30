using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server {
	class ClientInfo {
		public object locker = new object();
		public bool isRunning;
		public Thread thread;
		public TcpClient client;
		public NetworkStream stream;

		public void Send(byte[] data) {
			lock (locker) {
				Network.Protocol.BaseSend(stream, data);
			}
		}
	}
}
