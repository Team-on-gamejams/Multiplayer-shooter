﻿using System;
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
		bool isRunning;
		Thread clientThread;
		TcpClient client;
		object streamLocker = new object();
		NetworkStream stream;

		IPAddress ip;
		ushort port;

		Task updateTask;

		//ConcurrentQueue<GameObjectState[]> lastStates;

		public TCPClient() {
			//lastStates = new ConcurrentQueue<GameObjectState[]>();
		}

		public event Action<GameObjectState[]> OnWorldUpdate;

		public void Connect(string ip, ushort port) {
			this.ip = IPAddress.Parse(ip);
			this.port = port;
			isRunning = true;

			client = new TcpClient();
			client.Connect(ip, port);
			stream = client.GetStream();

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

			}
			throw new NotImplementedException();
		}

		void ProcessClient() {
			while (isRunning) {
				lock (streamLocker) {
					if (!stream.DataAvailable)
						continue;

					Protocol.BaseRecieve(stream, out byte[] data);

					if (data.Length % GameObjectState.OneObjectSize != 0) {
						Console.WriteLine($"Recieve {data.Length} bytes. BUT IT WRONG!!!11!");
						throw new Exception("Recieve smth wrong in UDPClient.ProcessClient()");
					}

					GameObjectState[] states = new GameObjectState[data.Length / GameObjectState.OneObjectSize];

					byte[] clone = new byte[GameObjectState.OneObjectSize];
					for(int i = 0; i < states.Length; ++i) {
						Array.Copy(data, i * GameObjectState.OneObjectSize, clone, 0, GameObjectState.OneObjectSize);
						states[i] = GameObjectState.Deserialize(clone);
					}

					updateTask = new Task(()=> {
						OnWorldUpdate.Invoke(states);
					});
					updateTask.Start();
					//lastStates.Enqueue(states);
				}
			}
		}
	}
}