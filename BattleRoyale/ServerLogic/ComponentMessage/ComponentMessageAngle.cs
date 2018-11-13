using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;
using ServerLogic.Components;

namespace ServerLogic.ComponentMessage {
	class ComponentMessageAngle : ComponentMessageBase{
		public short Angle { get; set; }

		public ComponentMessageAngle() : this(ComponentMessageType.None, 0) {

		}

		public ComponentMessageAngle(ComponentMessageType type, short angle) {
			ComponentMessageType = type;
			Angle = angle;
		}
	}
}
