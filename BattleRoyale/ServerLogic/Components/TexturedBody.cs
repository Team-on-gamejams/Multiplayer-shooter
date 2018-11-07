using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ServerLogic.Components {
	class TexturedBody : BaseComponent {
		public TextureId TextureId { get; protected set; }
		public Coord Pos { get; protected set; }
		public Size Size { get; protected set; }
		public short Angle { get; protected set; }

		public TexturedBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, TextureId textureId)
			: base(owner) {
			this.Pos = pos;
			this.Size = size;
			this.Angle = angle;
			this.TextureId = textureId;
			Owner.IsUpdated = true;
		}

		public override void ProcessMessage(IComponentMessage msg) {
			if(msg.ComponentMessageType == ComponentMessageType.AngleChanged) 
				ProcessAngleChanged(msg as ComponentMessageAngle);
		}

		void ProcessAngleChanged(ComponentMessageAngle msg) {
			Angle = msg.Angle;
			Owner.IsUpdated = true;
		}

		public void AppendCoords(Coord pos) {
			this.Pos.x = pos.x;
			this.Pos.y = pos.y;
		}

		public void AppendCoords(uint x, uint y) {
			this.Pos.x = x;
			this.Pos.y = y;
		}

		public void AppendCoords(Coord pos, short angle) {
			this.Pos.x = pos.x;
			this.Pos.y = pos.y;
			this.Angle = angle;
		}

		public void AppendCoords(uint x, uint y, short angle) {
			this.Pos.x = x;
			this.Pos.y = y;
			this.Angle = angle;
		}

		public void AddToCoords(Coord pos) {
			this.Pos.x += pos.x;
			this.Pos.y += pos.y;
		}

		public void AddToCoords(uint x, uint y) {
			this.Pos.x += x;
			this.Pos.y += y;
		}
	}
}
