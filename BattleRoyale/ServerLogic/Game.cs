using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;

namespace ServerLogic {
	public class Game : Common.Interfaces.IGame {
		public IGameSettings GameSettings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public IMatchSettings MatchSettings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public void AddPlayer(IPlayer player) {
			throw new NotImplementedException();
		}

		public IGameObject[] GetAllGameObjects() {
			throw new NotImplementedException();
		}

		public IPlayerDescriptor[] GetAllPlayers() {
			throw new NotImplementedException();
		}

		public IMatchStatitic GetMatchStatitic() {
			throw new NotImplementedException();
		}

		public IPlayerDescriptor GetPlayerDescriptor(IPlayer player) {
			throw new NotImplementedException();
		}

		public void RemovePlayer(IPlayer player) {
			throw new NotImplementedException();
		}

		public void ResetMap() {
			throw new NotImplementedException();
		}

		public void SetMap() {
			throw new NotImplementedException();
		}

		public void StartGame() {
			const int TICKS_PER_SECOND = 25;
			const int SKIP_TICKS = 1000 / TICKS_PER_SECOND;
			const int MAX_FRAMESKIP = 5;

			long next_game_tick = DateTime.Now.Ticks;
			int loops;
			float interpolation;

			bool game_is_running = true;
			while (game_is_running) {
				loops = 0;
				while (DateTime.Now.Ticks > next_game_tick && loops < MAX_FRAMESKIP) {
					//update_game();

					next_game_tick += SKIP_TICKS;
					loops++;
				}

				interpolation = (float)(DateTime.Now.Ticks + SKIP_TICKS - next_game_tick)
								/ (float)(SKIP_TICKS);
				//display_game(interpolation);
			}
		}

		public void StopGame() {
			throw new NotImplementedException();
		}
	}
}
