using System;
using System.Threading;

using Common;

namespace ServerLogic {
	public class Game {
		IServer server;
		GameContext gameContext = GameContext.GetGCState();
		Thread gameThread;

		public void StartGame(IServer server) {
			this.server = server;
			gameContext.SetServer(server);
			gameContext.LoadMap();

			gameThread = new Thread(() => {
				gameContext.StartGame();
			});
			gameThread.Start();
		}

		public void StopGame() {
			server.KickAllPlayers();
			gameContext.StopGame();
			//Wait until game really stop
			while (gameThread.IsAlive)
				Thread.Sleep(250);
		}
	}
}
