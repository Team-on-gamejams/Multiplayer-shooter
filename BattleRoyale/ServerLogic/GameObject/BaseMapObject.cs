using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerLogic.Components;

namespace ServerLogic.GameObject {
	class BaseMapObject : BaseGameObject{
		public BaseMapObject(GameObjectType type)
			: base(type, null) {

		}
	}
}
