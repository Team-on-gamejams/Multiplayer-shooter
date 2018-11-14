using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class SolidBody : TexturedBody{
		public bool IsSemisolid { get; private set; }

		public SolidBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, bool isSemisolid, TextureId textureId)
			: base(owner, pos, size, angle, textureId) {
			IsSemisolid = isSemisolid;
			Owner.IsUpdated = true;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Collide)
				ProcessCollideMsg(msg as CollideMessage);

			base.ProcessMessage(msg);
		}

		public void ProcessCollideMsg(CollideMessage collideMessage) {

		}
	}
}
