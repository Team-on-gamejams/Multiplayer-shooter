using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class ClientDisconnect {
		byte nothing;

		public ClientDisconnect() {
			nothing = 3;
		}

		static public byte OneObjectSize => 1;

		static public byte[] Serialize(ClientDisconnect state) {
			return new byte[] { state.nothing };
		}

		static public ClientDisconnect Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public ClientDisconnect Deserialize(byte[] bytes);");

			return new ClientDisconnect() {
				nothing = bytes[0],
			};
		}
	}
}
