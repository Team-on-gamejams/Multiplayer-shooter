using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.Components;

namespace ServerLogic.GameObject {
	class WallMapObject : BaseMapObject{
		public WallMapObject (Coord initPos, Size size, TextureId id)
			: base(GameObjectType.Wall) {
				base.components.Add(new SolidBody(this, initPos, size, 0, false, id));
		}
	}
}
