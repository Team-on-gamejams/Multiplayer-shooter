using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

using static System.Math;

namespace ServerLogic.Components {
	class Moveable : BaseComponent {
		SolidBody solidBody;
		byte speed;

		public Moveable(IGameObject owner, byte speed) : base(owner) {
			this.speed = speed;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			switch (msg.ComponentMessageType) {
				case ComponentMessageType.MoveForward:
					uint dX = (uint)Math.Round(Cos(solidBody.Angle) * speed),
				   dY = (uint)Math.Round(Sin(solidBody.Angle) * speed);

					solidBody.AddToCoords(dX, dY);
					break;
				case ComponentMessageType.MoveBackward:
					break;
				case ComponentMessageType.MoveLeft:
					break;
				case ComponentMessageType.MoveRight:
					break;
			}
		}

		public override bool CheckDependComponents() {
			solidBody = Owner.GetComponent<SolidBody>();

			return base.CheckDependComponents() && solidBody != null;
		}
	}
}
