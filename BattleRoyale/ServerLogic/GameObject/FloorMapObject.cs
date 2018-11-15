using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.Components;

namespace ServerLogic.GameObject {
	class FloorMapObject : BaseMapObject {
		public FloorMapObject(Coord initPos, Size size, TextureId id) 
			: base(GameObjectType.Floor){
				base.components.Add(new TexturedBody(this, initPos, size, 0, id));
		}
	}
}
