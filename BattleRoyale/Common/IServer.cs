using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Common {
	public interface IServer {
		void StartServer(string ip, ushort port);
		void StopServer();

		void DequeuePlayerAction();
		void SendWorldState();

		void AppendPlayer();
		void KickAllPlayers();
	}
}
