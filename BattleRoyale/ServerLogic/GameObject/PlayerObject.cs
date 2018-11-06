using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerLogic.Components;
using Common;

namespace ServerLogic.GameObject {
	class PlayerObject : BaseGameObject {
		public PlayerChampionType PlayerChampionType;

		public PlayerObject(Coord initPos, PlayerChampionType PlayerChampionType) : base(GameObjectType.Player, null){
			this.PlayerChampionType = PlayerChampionType;

			components.Add(new SolidBody(this, initPos, new Size(25, 25), 0, false, TextureId.Player));
			components.Add(new Moveable(this, 7));
		}
	}
}
