using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerLogic.Components {
	class DieableByDie : BaseComponent {
		public DieableByDie(IGameObject owner) : base(owner) {
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.Die)
				ProcessDieMessage();
		}

		void ProcessDieMessage() {
			Owner.Dispose();
			Owner.GetComponent<TexturedBody>().KillObject();
			Owner.IsUpdated = true;
		}
	}
}
