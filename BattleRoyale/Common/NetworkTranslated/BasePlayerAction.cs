using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class BasePlayerAction {
		public PlayerActionType actionType;

		//Use only if actionType == PlayerChangeAngle
		public short newAngle;

		//Fill and use only on server
		public ulong playerId;

		public BasePlayerAction() : this(PlayerActionType.None) {

		}

		public BasePlayerAction(PlayerActionType actionType) {
			this.actionType = actionType;
		}

		static public byte OneObjectSize => 3;

		static public byte[] Serialize(BasePlayerAction state) {
			byte[] bytes = new byte[OneObjectSize];

			bytes[0] = (byte)state.actionType;
			Array.Copy(BitConverter.GetBytes(state.newAngle), 0, bytes, 1, 2);

			return bytes;
		}

		static public BasePlayerAction Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public BasePlayerAction Deserialize(byte[] bytes);");

			var state = new BasePlayerAction((PlayerActionType)bytes[0]);
			state.newAngle = BitConverter.ToInt16(bytes, 1);

			return state;
		}
	}
}
