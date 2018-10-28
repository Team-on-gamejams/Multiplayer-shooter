using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServerLogic.Components {
	class SolidBody : BaseComponent{
		Coord pos;
		Size size;
		short angle;
		bool isSemisolid;

		public SolidBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, bool isSemisolid) : base(owner) {
			this.pos = pos;
			this.size = size;
			this.angle = angle;
			this.isSemisolid = isSemisolid;
		}

		public override void ProcessMessage(ComponentMessageBase msg) {
			switch (msg.ComponentMessageType) {
				case ComponentMessageType.None:
					break;
				default:
					break;
			}
		}
	}
}
