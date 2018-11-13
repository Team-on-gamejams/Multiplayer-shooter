using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class Coord : ICloneable{
		public uint x, y;

		public Coord() : this(0, 0) { }

		public Coord(uint x, uint y) {
			this.x = x;
			this.y = y;
		}

		public object Clone() {
			return MemberwiseClone();
		}
	}
}
