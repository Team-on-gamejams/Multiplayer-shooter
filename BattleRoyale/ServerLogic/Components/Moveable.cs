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
		readonly byte speed;
		//bool isAbleToMove;

		public Moveable(IGameObject owner, byte speed) : base(owner) {
			this.speed = speed;
			//isAbleToMove = true;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.MoveForward ||
				msg.ComponentMessageType == ComponentMessageType.MoveBackward ||
				msg.ComponentMessageType == ComponentMessageType.MoveLeft ||
				msg.ComponentMessageType == ComponentMessageType.MoveRight
			)
				ProcessMove(msg);
			//else if (msg.ComponentMessageType == ComponentMessageType.TickElapsed)
			//isAbleToMove = true;
		}

		void ProcessMove(IComponentMessage msg) {
			//if (isAbleToMove) {
			//isAbleToMove = false;

			switch (msg.ComponentMessageType) {
				case ComponentMessageType.MoveBackward:
					solidBody.AddToCoords(0, speed);
					break;
				case ComponentMessageType.MoveLeft:
					solidBody.AddToCoords(-speed, 0);
					break;
				case ComponentMessageType.MoveRight:
					solidBody.AddToCoords(speed, 0);
					break;
				case ComponentMessageType.MoveForward:
					solidBody.AddToCoords(0, -speed);
					break;
			}

			Owner.IsUpdated = true;
			//}
		}

		public override bool CheckDependComponents() {
			solidBody = Owner.GetComponent<SolidBody>();

			return base.CheckDependComponents() && solidBody != null;
		}
	}
}
