using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.Components;

namespace ServerLogic.ComponentMessage {
	class ComponentMessageBase : IComponentMessage {
		public ComponentMessageType ComponentMessageType { get; set; }

		public ComponentMessageBase() {
			ComponentMessageType = ComponentMessageType.None;
		}

		public ComponentMessageBase(ComponentMessageType type) {
			ComponentMessageType = type;
		}
	}
}
