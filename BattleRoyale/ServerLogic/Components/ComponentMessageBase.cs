using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServerLogic.Components {
	class ComponentMessageBase : IComponentMessage {
		public ComponentMessageType ComponentMessageType { get; set; }
	}
}
