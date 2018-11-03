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
			if (msg.ComponentMessageType == ComponentMessageType.MoveForward ||
				msg.ComponentMessageType == ComponentMessageType.MoveBackward ||
				msg.ComponentMessageType == ComponentMessageType.MoveLeft ||
				msg.ComponentMessageType == ComponentMessageType.MoveRight
			) {
				double angle = solidBody.Angle;

				switch (msg.ComponentMessageType) {
					case ComponentMessageType.MoveBackward:
						angle += 180;
						break;
					case ComponentMessageType.MoveLeft:
						angle -= 90;
						break;
					case ComponentMessageType.MoveRight:
						angle += 90;
						break;
				}
				angle = angle * Math.PI / 180;
				uint dX = (uint)Math.Round(Cos(angle) * speed),
					   dY = (uint)Math.Round(Sin(angle) * speed);
				solidBody.AddToCoords(dX, dY);
			}
		}

		public override bool CheckDependComponents() {
			solidBody = Owner.GetComponent<SolidBody>();

			return base.CheckDependComponents() && solidBody != null;
		}
	}
}
