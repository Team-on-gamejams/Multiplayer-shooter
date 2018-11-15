using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;
using ServerLogic.Components;

namespace ServerLogic.ComponentMessage {
	class ComponentMessageAngle : ComponentMessageBase{
		public short Angle { get; private set; }

		public ComponentMessageAngle(short angle) : base(ComponentMessageType.AngleChanged) {
			Angle = angle;
		}

		public ComponentMessageAngle(ComponentMessageType type, short angle) : base(type) {
			Angle = angle;
		}
	}
}
