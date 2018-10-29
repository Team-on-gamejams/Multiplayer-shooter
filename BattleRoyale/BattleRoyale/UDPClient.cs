using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace BattleRoyale {
	class UDPClient : Common.IClient{
		IPAddress ip;
		ushort port;

		public void Connect(string ip, ushort port) {
			this.ip = IPAddress.Parse(ip);
			this.port = port;
		}

		public void Disconnect() {
			throw new NotImplementedException();
		}

		public void SentPlayerAction() {
			throw new NotImplementedException();
		}

		public void GetWorldState() {
			throw new NotImplementedException();
		}
	}
}
