using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Common;

namespace Server {
	class UDPServer : Common.IServer {
		bool isRunning;
		Thread serverThread;

		IPAddress ip;
		ushort port;

		public UDPServer() {

		}

		public void AppendPlayer() {
			throw new NotImplementedException();
		}

		public void KickAllPlayers() {
			throw new NotImplementedException();
		}

		public void SendWorldState(GameObjectState[] worldState) {
			throw new NotImplementedException();
		}

		public void StartServer(string ip, ushort port) {
			this.ip = IPAddress.Parse(ip);
			this.port = port;
			isRunning = true;

			serverThread = new Thread(()=> {
				ProcessServer();
			});
		}

		public void StopServer() {
			isRunning = false;
			while (serverThread.IsAlive)
				Thread.Sleep(250);
		}

		public bool TryDequeuePlayerAction(out BasePlayerAction playerAction) {
			throw new NotImplementedException();
		}

		void ProcessServer() {
			while (isRunning) {

			}
		}
	}
}
