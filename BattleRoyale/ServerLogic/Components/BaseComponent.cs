using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ServerLogic.GameObject;

namespace ServerLogic.Components {
	abstract class BaseComponent : Common.IComponent{
		public IGameObject Owner { get; protected set; }

		protected BaseComponent(IGameObject owner) {
			this.Owner = owner;
			if (!CheckDependComponents())
				throw new ApplicationException("No linked component");
		}

		public abstract void ProcessMessage(Common.IComponentMessage msg);

		public virtual bool CheckDependComponents() {
			return true;
		}
	}
}
