using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Common {
	public interface IServer {
		void StartServer(string ip, ushort port);
		void StopServer();

		bool TryDequeuePlayerAction(out BasePlayerAction playerAction);
		void SendWorldState(GameObjectState[] worldState);

		void KickAllPlayers();
	}
}
