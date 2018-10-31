﻿using System;
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
			for (byte i = 0; i < 10; ++i) {
				for (byte j = 0; j < 10; ++j) {
					if (i == 0 || j == 0 || i == 9 || j == 9)
						map.Add(new WallMapObject(new Coord((uint)(i * 50), (uint)(j * 50)), TextureId.DungeonWall));
					else
						map.Add(new FloorMapObject(new Coord((uint)(i * 50), (uint)(j * 50)), TextureId.DungeonFloor));
				}
			}

			players.Add(new PlayerObject(new Coord(113, 113)));
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
			const int cps = 24;
			const int maxFps = 24;
			const int skipTickcps = 1000 / cps;
			const int skipTickfps = 1000 / maxFps;
			const int maxFrameSkip = 10;

			int nextTickcps = Environment.TickCount;
			int nextTickfps = Environment.TickCount;
			int loops;

			while (isRunning) {
				loops = 0;
				while (Environment.TickCount > nextTickcps && loops < maxFrameSkip) {
					Update();

					nextTickcps += skipTickcps;
					loops++;
				}

				if (Environment.TickCount > nextTickfps) {
					Display();
					nextTickfps += skipTickfps;
				}
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

		bool a = false;
		void Update() {
			var c = players[0].GetComponent<SolidBody>();
			if (a)
				c.AppendCoords(c.Pos.x - 50, c.Pos.y);
			else
				c.AppendCoords(c.Pos.x + 50, c.Pos.y);

			a = !a;

			ReadPlayersInput();
			ProcessMessages();


			//RemoveDisposedObjects();
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
