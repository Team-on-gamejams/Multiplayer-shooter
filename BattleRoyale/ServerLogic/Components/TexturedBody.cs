using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServerLogic.Components {
	class TexturedBody : BaseComponent {
		protected TextureId textureId;
		protected Coord pos;
		protected Size size;
		protected short angle;

		public TexturedBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, TextureId textureId)
			: base(owner) {
			this.pos = pos;
			this.size = size;
			this.angle = angle;
			this.textureId = textureId;
		}

		public override void ProcessMessage(ComponentMessageBase msg) {
			switch (msg.ComponentMessageType) {
				case ComponentMessageType.None:
					break;
				default:
					break;
			}
		}

		public void AppendCoords(Coord pos, short angle) {
			this.pos.x = pos.x;
			this.pos.y = pos.y;
			this.angle = angle;
		}
	}
}
