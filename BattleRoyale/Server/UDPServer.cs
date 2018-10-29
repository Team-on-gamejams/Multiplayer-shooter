using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace Server {
	class UDPServer : Common.IServer {
		IPAddress ip;
		ushort port;

		public void StartServer(string ip, ushort port) {
			this.ip = IPAddress.Parse(ip);
			this.port = port;
		}

		public void StopServer() {
			throw new NotImplementedException();
		}

		public void DequeuePlayerAction() {
			throw new NotImplementedException();
		}

		public void SendWorldState() {
			throw new NotImplementedException();
		}

		public void AppendPlayer() {
			throw new NotImplementedException();
		}

		public void KickAllPlayers() {
			throw new NotImplementedException();
		}
	}
}
