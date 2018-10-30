﻿using System;
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
			this.Pos.x = pos.x;
			this.Pos.y = pos.y;
			this.Angle = angle;
		}
	}
}