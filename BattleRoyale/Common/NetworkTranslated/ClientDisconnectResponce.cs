using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class ClientDisconnectResponce {
		byte nothing;

		public ClientDisconnectResponce() {
			nothing = 3;
		}

		static public byte OneObjectSize => 1;

		static public byte[] Serialize(ClientDisconnectResponce state) {
			return new byte[] { state.nothing };
		}

		static public ClientDisconnectResponce Deserialize(byte[] bytes) {
			//Console.WriteLine(bytes.Length.ToString() + "  " + OneObjectSize.ToString());
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public ClientDisconnectResponce Deserialize(byte[] bytes);");

			return new ClientDisconnectResponce() {
				nothing = bytes[0],
			};
		}
	}
}
