using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerLogic.Components {
	class Projectile : BaseComponent {
		SolidBody solidBody;
		readonly byte speed;

		public Projectile(IGameObject owner, byte speed) : base(owner) {
			this.speed = speed;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if (msg.ComponentMessageType == ComponentMessageType.TickElapsed)
				ProcessTickElapsed(msg);
		}

		void ProcessTickElapsed(IComponentMessage msg) {
			double angle = solidBody.Angle;
			angle = angle * Math.PI / 180;
			int dX = (int)Math.Round(Math.Cos(angle) * speed),
				   dY = (int)Math.Round(Math.Sin(angle) * speed);
			solidBody.AddToCoords(dX, dY);
			Owner.IsUpdated = true;
		}

		public override bool CheckDependComponents() {
			solidBody = Owner.GetComponent<SolidBody>();

			return base.CheckDependComponents() && solidBody != null;
		}
	}
}
