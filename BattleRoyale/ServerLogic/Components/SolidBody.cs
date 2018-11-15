using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class SolidBody : TexturedBody{
		public IGameObject IgnoreCollide { get; private set; }
		public bool IsSemisolid { get; private set; }

		public SolidBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, bool isSemisolid, TextureId textureId, IGameObject _IgnoreCollide)
			: base(owner, pos, size, angle, textureId) {
			IsSemisolid = isSemisolid;
			Owner.IsUpdated = true;
			IgnoreCollide = _IgnoreCollide;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Collide)
				ProcessCollideMsg(msg as CollideMessage);

			base.ProcessMessage(msg);
		}

		public void ProcessCollideMsg(CollideMessage collideMessage) {
			if (
				(IsSemisolid && (collideMessage.CollideWith.GetComponent<SolidBody>()?.IsSemisolid ?? false)) ||
				(collideMessage.CollideWith == IgnoreCollide)
			)
				return;

			Pos.Set(PrevPos);
		}
	}
}
