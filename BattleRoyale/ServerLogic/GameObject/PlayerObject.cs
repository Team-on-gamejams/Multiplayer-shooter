using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerLogic.Components;
using Common;

namespace ServerLogic.GameObject {
	class PlayerObject : BaseGameObject {
		public PlayerObject(Coord initPos) : base(GameObjectType.Player, null){
			base.components.Add(new SolidBody(this, initPos, new Size(25, 25), 0, false));

		}
	}
}
