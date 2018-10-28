using System;
using System.Threading;

using Common;

namespace ServerLogic {
	public class Game {
		IServer server;
		GameContext gameContext = GameContext.GetGCState();

		public void StartGame(IServer server) {
			this.server = server;
		}

		public void StopGame() {

		}
	}
}
