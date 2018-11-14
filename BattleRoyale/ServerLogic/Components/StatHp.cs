using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class StatHp : BaseComponent {
		short currHp, maxHP;

		public StatHp(IGameObject owner, short currHp, short maxHP) : base(owner) {
			this.currHp = currHp;
			this.maxHP = maxHP;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.TakeDmg)
				ProcessTakeDMGMessage(msg as TakeDmgMessage);
			else if (msg.ComponentMessageType == ComponentMessageType.Die)
				ProcessDieMessage();
		}

		public void ProcessTakeDMGMessage(TakeDmgMessage message) {
			currHp -= message.Dmg;

			if (currHp < 0)
				currHp = 0;

			if (currHp == 0 && !message.CanKill)
				currHp = 1;
		}

		public void ProcessDieMessage() {
			currHp = 0;
		}
	}
}
