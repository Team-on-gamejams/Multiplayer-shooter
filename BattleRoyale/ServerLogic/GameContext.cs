using System;
using System.Collections.Generic;

using ServerLogic.GameObject;

namespace ServerLogic {
	class GameContext {
		static GameContext gameContext;

		static public GameContext GetGCState() {
			return gameContext;
		}

		static GameContext() {
			gameContext = new GameContext();
		}

		private GameContext() {
			map = new List<BaseGameObject>();
			players = new List<PlayerObject>();
			gameObjects = new List<BaseGameObject>();
		}

		List<BaseGameObject> map;
		List<PlayerObject> players;
		List<BaseGameObject> gameObjects;

		public void StartGame() {

		}

		public void StopGame() {

		}

		public void ProcessGame() {

		}
	}
}
