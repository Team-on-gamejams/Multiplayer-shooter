using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class Coord {
		public double x, y;

		public Coord() : this(0, 0) { }

		public Coord(double x, double y) {
			this.x = x;
			this.y = y;
		}
	}
}
