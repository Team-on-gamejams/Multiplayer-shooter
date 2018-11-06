using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class ClientConnectResponce {
		public ulong playerId;
		//Fill and use only at server side
		public GameObjectState[] initialWorldState;

		static public byte OneObjectSize => 8;

		static public byte[] Serialize(ClientConnectResponce state) {
			byte[] bytes = new byte[OneObjectSize];

			Array.Copy(BitConverter.GetBytes(state.playerId), 0, bytes, 0, 8);

			return bytes;
		}

		static public ClientConnectResponce Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public ClientConnectResponce Deserialize(byte[] bytes);");

			ClientConnectResponce rez = new ClientConnectResponce {
				playerId = (ulong)BitConverter.ToInt64(bytes, 0)
			};

			return rez;
		}
	}
}
