using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class DieOnCollide : BaseComponent {
		public IGameObject IgnoreCollide { get; private set; }

		public DieOnCollide(IGameObject owner, IGameObject ignoreCollide) : base(owner) {
			IgnoreCollide = ignoreCollide;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Collide)
				ProcessCollideMsg(msg as CollideMessage);
		}

		public void ProcessCollideMsg(CollideMessage collideMessage) {
			if(collideMessage.CollideWith != IgnoreCollide)
				Owner.SendMessage(new ComponentMessageBase(ComponentMessageType.Die));
		}
	}
}
