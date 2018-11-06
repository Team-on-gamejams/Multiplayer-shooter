using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using Common;
using Network;

namespace BattleRoyale {
	class TCPClient : Common.IClient {
		public ulong PlayerId { get; set; }
		bool isRunning;
		Thread clientThread;
		TcpClient client;
		object streamLocker = new object();
		NetworkStream stream;

		IPAddress ip;
		ushort port;

		Task updateTask;

		public event Action<GameObjectState[]> OnWorldUpdate;

		//ConcurrentQueue<GameObjectState[]> lastStates;

		public TCPClient() {
			//lastStates = new ConcurrentQueue<GameObjectState[]>();
		}

		public void Connect(string ip, ushort port) {
			this.ip = IPAddress.Parse(ip);
			this.port = port;
			isRunning = true;

			client = new TcpClient();
			client.Connect(ip, port);
			stream = client.GetStream();

			lock (streamLocker) {
				Protocol.BaseSend(stream, PacketType.ClientConnect, ClientConnect.Serialize(
					new ClientConnect() {
						playerChampionType = PlayerChampionType.Jade
					})
				);

				while (!stream.DataAvailable)
					Thread.Sleep(1);

				byte[] data = new byte[ClientConnectResponce.OneObjectSize];
				ClientConnectResponce responce;

				PacketType type = Protocol.BaseRecieve(stream, out data);
				if (type == PacketType.ClientConnectResponce) {
					responce = ClientConnectResponce.Deserialize(data);
					PlayerId = responce.playerId;
				}
				else
					throw new Exception("Recieve smth wrong in Client.Connect()");
			}

			clientThread = new Thread(() => {
				ProcessClient();
			});
			clientThread.Start();
		}

		bool IsDisconnected = false;
		public void Disconnect() {
			if (IsDisconnected)
				return;

			IsDisconnected = true;
			//Console.WriteLine("Start disconnect");

			isRunning = false;
			while (clientThread.IsAlive)
				Thread.Sleep(100);
			//Console.WriteLine("clientThread stopped");

			lock (streamLocker) {
				Protocol.BaseSend(stream, PacketType.ClientDisconnect, ClientDisconnect.Serialize(
					new ClientDisconnect() {
						
					})
				);
				//Console.WriteLine("Send ClientDisconnect");

				while (!stream.DataAvailable)
					Thread.Sleep(100);

				//Console.WriteLine("Receive ClientDisconnectResponce");

				byte[] data = new byte[ClientDisconnectResponce.OneObjectSize];
				ClientDisconnectResponce responce;

				PacketType type;
				byte maxReadLoops = 100;
				do {
					type = Protocol.BaseRecieve(stream, out data);
					if (--maxReadLoops == 0)
						break;
				} while (type != PacketType.ClientDisconnectResponce);

				if (type == PacketType.ClientDisconnectResponce) {
					responce = ClientDisconnectResponce.Deserialize(data);
					//Console.WriteLine("Deserialize ClientDisconnectResponce");
				}
				else 
					throw new Exception("Wait for PacketType.ClientDisconnectResponce, but maxReadLoops(now 100) reached 0");
			}

			stream.Close();
			client.Close();

			//Console.WriteLine("End disconnect");
		}

		//public GameObjectState[] GetWorldState() {
		//	if (lastStates.TryDequeue(out GameObjectState[] result))
		//		return result;
		//	return null;
		//}

		public void SentPlayerAction(BasePlayerAction playerAction) {
			lock (streamLocker) {
				Protocol.BaseSend(stream, PacketType.PlayerAction, BasePlayerAction.Serialize(playerAction));
			}
		}

		void ProcessClient() {
			while (isRunning) {
				lock (streamLocker) {
					if (!stream.DataAvailable)
						continue;

					PacketType type = Protocol.BaseRecieve(stream, out byte[] data);

					if (type == PacketType.WorldState) {
						if (data.Length % GameObjectState.OneObjectSize != 0) {
							throw new Exception("Recieve smth wrong in UDPClient.ProcessClient()");
						}

						GameObjectState[] states = new GameObjectState[data.Length / GameObjectState.OneObjectSize];

						byte[] clone = new byte[GameObjectState.OneObjectSize];
						for (int i = 0; i < states.Length; ++i) {
							Array.Copy(data, i * GameObjectState.OneObjectSize, clone, 0, GameObjectState.OneObjectSize);
							states[i] = GameObjectState.Deserialize(clone);
						}

						updateTask = new Task(() => {
							OnWorldUpdate.Invoke(states);
						});
						updateTask.Start();
						//lastStates.Enqueue(states);
					}
				}
			}
		}
	}
}
