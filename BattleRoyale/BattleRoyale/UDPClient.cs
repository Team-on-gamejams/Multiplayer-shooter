using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Common;

namespace BattleRoyale {
	class UDPClient : Common.IClient{
		bool isRunning;
		Thread clientThread;

		IPAddress ip;
		ushort port;

		public UDPClient() {

		}

		public void Connect(string ip, ushort port) {
			this.ip = IPAddress.Parse(ip);
			this.port = port;
			isRunning = true;

			clientThread = new Thread(() => {
				ProcessClient();
			});
		}

		public void Disconnect() {
			isRunning = false;
			while (clientThread.IsAlive)
				Thread.Sleep(250);
		}

		public GameObjectState[] GetWorldState() {
			throw new NotImplementedException();
		}

		public void SentPlayerAction(BasePlayerAction playerAction) {
			throw new NotImplementedException();
		}

		void ProcessClient() {
			while (isRunning) {

			}
		}
	}
}
