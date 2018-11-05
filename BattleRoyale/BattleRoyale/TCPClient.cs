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
					Console.WriteLine(PlayerId);
				}
				else
					throw new Exception("Recieve smth wrong in Client.Connect()");
			}

			clientThread = new Thread(() => {
				ProcessClient();
			});
			clientThread.Start();
		}

		public void Disconnect() {
			isRunning = false;
			while (clientThread.IsAlive)
				Thread.Sleep(250);
			stream.Close();
			client.Close();
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
							Console.WriteLine($"Recieve {data.Length} bytes. BUT IT WRONG!!!11!");
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
