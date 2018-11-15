using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class DealDMGOnCollide : BaseComponent {
		public IGameObject IgnoreCollide { get; private set; }
		public bool CanKill { get; private set; }
		public short Dmg { get; private set; }

		public DealDMGOnCollide(IGameObject owner, short _dmg, bool canKill, IGameObject ignoreCollide) : base(owner) {
			Dmg = _dmg;
			CanKill = canKill;
			IgnoreCollide = ignoreCollide;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Collide)
				ProcessCollideMsg(msg as CollideMessage);
		}

		public void ProcessCollideMsg(CollideMessage collideMessage) {
			if (collideMessage.CollideWith != IgnoreCollide)
				collideMessage.CollideWith.SendMessage(new TakeDmgMessage(CanKill, Dmg));
		}
	}
}
