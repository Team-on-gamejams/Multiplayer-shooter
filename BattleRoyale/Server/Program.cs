using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic;

namespace Server {
	class Program {
		static void Main(string[] args) {
			IServer server = new UDPServer();
			Game game = new Game();

			server.StartServer("127.0.0.1", 65000);
			game.StartGame(server);

			while (Console.ReadKey().KeyChar != 'q');

			game.StopGame();
			server.StopServer();
		}
	}
}
