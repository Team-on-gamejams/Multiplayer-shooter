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
			throw new NotImplementedException();
		}

		public void StopGame() {
			throw new NotImplementedException();
		}
	}
}
