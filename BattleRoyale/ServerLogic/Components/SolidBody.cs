using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServerLogic.Components {
	class SolidBody : TexturedBody{
		bool isSemisolid;

		public SolidBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, bool isSemisolid, TextureId textureId)
			: base(owner, pos, size, angle, textureId) {
			this.isSemisolid = isSemisolid;
		}

		public override void ProcessMessage(ComponentMessageBase msg) {
			switch (msg.ComponentMessageType) {
				case ComponentMessageType.None:
					break;
				default:
					break;
			}

			base.ProcessMessage(msg);
		}
	}
}
