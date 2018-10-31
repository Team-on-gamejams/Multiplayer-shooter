using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Common;
using Network;


namespace Server {
	class TCPServer : Common.IServer {
		TcpListener server;
		bool isRunning;
		Thread serverThread;
		object clientsLocker = new object();
		List<ClientInfo> clients;

		IPAddress ip;
		ushort port;

		public TCPServer() {
			clients = new List<ClientInfo>();
		}

		public void StartServer(string _ip, ushort _port) {
			this.ip = IPAddress.Parse(_ip);
			this.port = _port;
			isRunning = true;

			server = new TcpListener(ip, port);
			serverThread = new Thread(() => {
				ProcessServer();
			});

			server.Start();
			serverThread.Start();
		}

		public void StopServer() {
			isRunning = false;
			while (serverThread.IsAlive)
				Thread.Sleep(250);
			server.Stop();
		}

		public void KickAllPlayers() {
			lock (clientsLocker) {
				foreach (var client in clients) {
					if (client.isRunning)
						client.isRunning = false;
				}
			}
		}

		public bool TryDequeuePlayerAction(out BasePlayerAction playerAction) {
			throw new NotImplementedException();
		}

		void ProcessServer() {
			while (isRunning) {
				TcpClient client = server.AcceptTcpClient();
				Thread clientThread = new Thread(new ParameterizedThreadStart(ProcessClient));
				clientThread.Start(client);
			}
		}

		void ProcessClient(object _client) {
			if (!(_client is TcpClient))
				return;

			ClientInfo clientInfo = new ClientInfo();
			clientInfo.isRunning = true;
			clientInfo.thread = Thread.CurrentThread;
			clientInfo.client = _client as TcpClient;
			clientInfo.stream = clientInfo.client.GetStream();

			lock (clientsLocker) {
				clients.Add(clientInfo);
			}

			byte[] data;

			while(clientInfo.isRunning) {
				lock (clientInfo.locker) {
					if (!clientInfo.stream.DataAvailable)
						continue;

				}
			}

			lock (clientInfo.locker) {
				clientInfo.stream.Close();
				clientInfo.client.Close();
			}
		}

		public void SendWorldState(GameObjectState[] worldState) {
			List<byte> data = new List<byte>();
			foreach (var state in worldState)
				data.AddRange(GameObjectState.Serialize(state));

			Console.WriteLine($"Send {data.Count} bytes {new DateTime(worldState[0].ticks).ToLongTimeString()}    {worldState.Length}");
			lock (clientsLocker) {
				foreach (var c in clients) {
					lock (c.locker)
						c.Send(data.ToArray());
				}
			}
		}
	}
}
