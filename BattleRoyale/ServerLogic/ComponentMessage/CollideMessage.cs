using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServerLogic.ComponentMessage {
	class CollideMessage : ComponentMessageBase {
		public GameObject.BaseGameObject CollideWith { get; private set; }

		public CollideMessage(GameObject.BaseGameObject collideWith) : base(ComponentMessageType.Collide) {
			CollideWith = collideWith;
		}
	}
}
