using System;
using System.Collections.Generic;

using Common;
using ServerLogic.Components;
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
			toRemove = new List<BaseGameObject>();
			isRunning = true;
		}
		#endregion

		IServer server;
		List<BaseMapObject> map;
		List<PlayerObject> players;
		List<BaseGameObject> gameObjects;
		List<BaseGameObject> toRemove;

		bool isRunning;

		public void SetServer(IServer server) {
			this.server = server;
		}

		public void LoadMap() {

		}

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

		void Display() {
			List<GameObjectState> states = new List<GameObjectState>(map.Count + players.Count + gameObjects.Count);
			Components.TexturedBody texturedObj;
			foreach (var i in map) {
				texturedObj = i.GetComponent<Components.TexturedBody>();
				if (texturedObj != null) {
					states.Add(new GameObjectState(
						texturedObj.TextureId, i.Id,
						texturedObj.Pos, texturedObj.Angle,
						texturedObj.Size
					));
				}
			}

			foreach (var i in players) {
				texturedObj = i.GetComponent<Components.TexturedBody>();
				if (texturedObj != null) {
					states.Add(new GameObjectState(
						texturedObj.TextureId, i.Id,
						texturedObj.Pos, texturedObj.Angle,
						texturedObj.Size
					));
				}
			}

			foreach (var i in gameObjects) {
				texturedObj = i.GetComponent<Components.TexturedBody>();
				if (texturedObj != null) {
					states.Add(new GameObjectState(
						texturedObj.TextureId, i.Id,
						texturedObj.Pos, texturedObj.Angle,
						texturedObj.Size
					));
				}
			}

			server.SendWorldState(states.ToArray());
		}

		void Update() {
			ReadPlayersInput();
			ProcessMessages();


			RemoveDisposedObjects();
		}

		void ReadPlayersInput() {

		}

		void ProcessMessages() {
			foreach (var i in gameObjects)
				i.Process();
			foreach (var i in players)
				i.Process();
			//foreach (var i in map) 
			//	i.Process();
		}

		void RemoveDisposedObjects() {
			foreach (var i in gameObjects)
				if (i.IsDisposed())
					toRemove.Add(i);
			if (toRemove.Count == 0) {
				foreach (var i in toRemove)
					gameObjects.Remove(i);
				toRemove.Clear();
			}

			foreach (var i in players)
				if (i.IsDisposed())
					toRemove.Add(i);
			if (toRemove.Count == 0) {
				foreach (var i in toRemove)
					players.Remove(i as PlayerObject);
				toRemove.Clear();
			}

			//foreach (var i in map)
			//	if (i.IsDisposed())
			//		toRemove.Add(i);
			//if (toRemove.Count == 0) {
			//	foreach (var i in toRemove)
			//		map.Remove(i as BaseMapObject);
			//	toRemove.Clear();
			//}
		}
	}
}
