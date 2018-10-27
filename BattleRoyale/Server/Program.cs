using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

using Common.Interfaces;
using ServerLogic;

namespace Server {
	class Program {
		static void Main(string[] args) {
			IGame game = new Game();
			IServer networkServer = new UDPGameServer();
			networkServer.SetGame(game);

			game.GameSettings = null;
			game.MatchSettings = null;
			game.StartGame();

			networkServer.StartServer();
			while (Console.ReadKey().KeyChar != 'q')
				Thread.Sleep(1000);

			game.StopGame();
			networkServer.StopServer();
		}
	}
}
