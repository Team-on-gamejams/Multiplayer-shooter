using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.ComponentMessage;

namespace ServerLogic.Components {
	class TexturedBody : BaseComponent {
		public TextureId TextureId { get; protected set; }
		public Coord Pos { get; protected set; }
		protected Coord PrevPos { get; set; }
		public Size Size { get; protected set; }
		public short Angle { get; protected set; }

		public TexturedBody(GameObject.BaseGameObject owner, Coord pos, Size size, short angle, TextureId textureId)
			: base(owner) {
			this.Pos = pos;
			PrevPos = new Coord(pos.x, pos.y);
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
			PrevPos.Set(Pos);
			this.Pos.x = pos.x;
			this.Pos.y = pos.y;
		}

		public void AppendCoords(uint x, uint y) {
			PrevPos.Set(Pos);
			this.Pos.x = x;
			this.Pos.y = y;
		}

		public void AppendCoords(Coord pos, short angle) {
			PrevPos.Set(Pos);
			this.Pos.x = pos.x;
			this.Pos.y = pos.y;
			this.Angle = angle;
		}

		public void AppendCoords(int x, int y, short angle) {
			PrevPos.Set(Pos);
			this.Pos.x = (uint)(this.Pos.x + x);
			this.Pos.y = (uint)(this.Pos.y + y);
			this.Angle = angle;
		}

		public void AddToCoords(Coord pos) {
			PrevPos.Set(Pos);
			this.Pos.x += pos.x;
			this.Pos.y += pos.y;
		}

		public void AddToCoords(int x, int y) {
			PrevPos.Set(Pos);
			this.Pos.x = (uint)(this.Pos.x + x);
			this.Pos.y = (uint)(this.Pos.y + y);
		}

		public bool IsCollide(TexturedBody body) {
			return	body.Pos.x < Pos.x && Pos.x < body.Pos.x + body.Size.width &&
					body.Pos.y < Pos.y && Pos.y < body.Pos.y + body.Size.height
				;
		}

		public void KillObject() {
			TextureId = TextureId.None;
		}
	}
}
