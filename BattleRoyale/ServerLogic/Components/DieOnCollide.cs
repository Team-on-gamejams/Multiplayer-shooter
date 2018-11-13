using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerLogic.Components {
	class DieOnCollide : BaseComponent {
		public DieOnCollide(IGameObject owner) : base(owner) {
		}

		public override void ProcessMessage(IComponentMessage msg) {
			throw new NotImplementedException();
		}
	}
}
