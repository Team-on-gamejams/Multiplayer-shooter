using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class Size : ICloneable {
		public uint width, height;

		public Size() : this(0, 0) { }

		public Size(uint width, uint height) {
			this.width = width;
			this.height = height;
		}

		public object Clone() {
			return MemberwiseClone();
		}
	}
}
