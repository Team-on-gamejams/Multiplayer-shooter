using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.GameObject;

namespace ServerLogic.Components {
	abstract class BaseComponent {
		BaseGameObject owner;

		protected BaseComponent(BaseGameObject owner) {
			this.owner = owner;
		}

		public abstract void ProcessMessage(ComponentMessageBase msg);

		public virtual bool CheckDependComponents() {
			return true;
		}
	}
}
