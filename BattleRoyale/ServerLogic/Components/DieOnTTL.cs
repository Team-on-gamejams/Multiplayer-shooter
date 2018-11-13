using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerLogic.Components {
	class DieOnTTL : BaseComponent {
		ushort ttl;
		ushort lifetime;

		public DieOnTTL(IGameObject owner, ushort ticksToLive) : base(owner) {
			ttl = ticksToLive;
			lifetime = 0;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.TickElapsed) {
				++lifetime;

				if (lifetime == ttl) {
					Owner.SendMessage(new ComponentMessageBase() { ComponentMessageType = ComponentMessageType.Die });
				}
			}
		}
	}
}
