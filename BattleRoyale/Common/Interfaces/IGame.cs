using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces {
	public interface IGame {
		void AddPlayer(IPlayer player);
		void RemovePlayer(IPlayer player);
		IPlayerDescriptor GetPlayerDescriptor(IPlayer player);

		void SetMap();
		void ResetMap();
		void StartGame();
		void StopGame();

		IPlayerDescriptor[] GetAllPlayers();
		IGameObject[] GetAllGameObjects();

		IGameSettings GameSettings { get; set; }
		IMatchSettings MatchSettings { get; set; }
		IMatchStatitic GetMatchStatitic();
	}
}
