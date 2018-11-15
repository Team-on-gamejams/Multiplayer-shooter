using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ServerLogic.Components;

namespace ServerLogic.GameObject {
	class PistolBullet : BaseGameObject {
		public PistolBullet(BaseGameObject parent) : base(GameObjectType.Projectile, parent) {
			Size bulletSize = new Size(30, 30);
			Coord startBulletPos = (Coord)parent.GetComponent<SolidBody>().Pos.Clone();
			startBulletPos.x += parent.GetComponent<SolidBody>().Size.width / 2 - bulletSize.width / 2;
			startBulletPos.y += parent.GetComponent<SolidBody>().Size.height / 2 - bulletSize.height / 2;

			components.Add(new SolidBody(this,
				startBulletPos,
				bulletSize,
				parent.GetComponent<SolidBody>().Angle,
				true,
				TextureId.PistolBullet,
				Parent
			));

			components.Add(new Projectile(this, 5));


			components.Add(new DealDMGOnCollide(this, 110, true, Parent));

			components.Add(new DieOnCollide(this, Parent));
			components.Add(new DieOnTTL(this, 100));

			components.Add(new DieableByDieMessage(this));
		}
	}
}
