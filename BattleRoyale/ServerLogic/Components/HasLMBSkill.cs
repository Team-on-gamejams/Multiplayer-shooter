using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerLogic.Components {
	class HasLMBSkill : BaseComponent {
		Action<IComponentMessage> actionSkill;

		public HasLMBSkill(IGameObject owner, Action<IComponentMessage> useSkill) : base(owner) {
			actionSkill = useSkill;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.SkillLMB)
				ProcessLMBSkillMessage(msg);
		}

		void ProcessLMBSkillMessage(IComponentMessage componentMessage) {
			actionSkill(componentMessage);
		}
	}
}
