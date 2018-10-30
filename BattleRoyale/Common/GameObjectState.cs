using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class GameObjectState {
		public TextureId TextureId { get; set; }
		public ulong Id { get; set; }
		public Coord Pos { get; set; }
		public short Angle { get; set; }
		public Size Size { get; set; }

		public GameObjectState(TextureId textureId, ulong id, Coord pos, short angle, Size size) {
			TextureId = textureId;
			Id = id;
			Pos = pos;
			Angle = angle;
			Size = size;
		}
	}
}
