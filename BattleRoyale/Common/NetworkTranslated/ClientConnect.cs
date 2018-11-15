using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class ClientConnect {
		public PlayerChampionType playerChampionType;

		static public byte OneObjectSize => 1;

		static public byte[] Serialize(ClientConnect state) {
			byte[] bytes = new byte[OneObjectSize];

			bytes[0] = (byte)state.playerChampionType;

			return bytes;
		}

		static public ClientConnect Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public ClientConnect Deserialize(byte[] bytes);");

			ClientConnect rez = new ClientConnect {
				playerChampionType = (PlayerChampionType)bytes[0]
			};

			return rez;
		}
	}
}
