using System;
using System.Collections.Generic;

using ServerLogic.GameObject;

namespace ServerLogic {
	class GameContext {
#region Singletone
		static GameContext gameContext;

		static public GameContext GetGCState() {
			return gameContext;
		}

		static GameContext() {
			gameContext = new GameContext();
		}

		private GameContext() {
			map = new List<BaseMapObject>();
			players = new List<PlayerObject>();
			gameObjects = new List<BaseGameObject>();
			isRunning = true;
		}
#endregion

		List<BaseMapObject> map;
		List<PlayerObject> players;
		List<BaseGameObject> gameObjects;

		bool isRunning;

		public void StartGame() {
			ProcessGame();
		}

		public void StopGame() {
			isRunning = false;
		}

		public void ProcessGame() {
			//const int fps = 30;
			//const int skipTick = 1000 / fps;
			//int nextTick = Environment.TickCount;
			//int sleepTime = 0;

			//while (isRunning) {
			//	Update();
			//	Display();

			//	nextTick += skipTick;
			//	sleepTime = nextTick - Environment.TickCount;
			//	if (sleepTime >= 0) {
			//		System.Threading.Thread.Sleep(sleepTime);
			//	}
			//	else {
			//		// Shit, we are running behind!
			//	}
			//}

			//Calculations per second (like fps)
			const int cps = 30;
			const int skipTick = 1000 / cps;
			const int maxFrameSkip = 10;

			int nextTick = Environment.TickCount;
			int loops;

			while (isRunning) {
				loops = 0;
				while (Environment.TickCount > nextTick && loops < maxFrameSkip) {
					Update();

					nextTick += skipTick;
					loops++;
				}

				Display();
			}
		}

		void Update() {

		}

		void Display() {

		}
	}
}
