using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class DealDMGOnCollide : BaseComponent {
		public DealDMGOnCollide(IGameObject owner) : base(owner) {

		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Collide)
				ProcessCollideMsg(msg as CollideMessage);
		}

		public void ProcessCollideMsg(CollideMessage collideMessage) {

		}
	}
}
