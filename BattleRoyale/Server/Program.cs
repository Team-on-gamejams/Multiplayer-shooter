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
			IServer server = new TCPServer();
			Game game = new Game();

			string ip;

			Console.Write("Ip: ");
			ip = Console.ReadLine();
			if (ip.Length <= 2)
				ip = "127.0.0.1";
			Console.WriteLine(ip);

			server.StartServer(ip, 65000);
			game.StartGame(server);

			while (Console.ReadKey().KeyChar != 'q');

			game.StopGame();
			server.StopServer();
		}
	}
}
