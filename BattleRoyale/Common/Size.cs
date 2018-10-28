using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class Size {
		public double width, height;

		public Size() : this(0, 0) { }

		public Size(double width, double height) {
			this.width = width;
			this.height = height;
		}
	}
}
