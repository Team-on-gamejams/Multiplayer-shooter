using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class BasePlayerAction {
		public PlayerActionType actionType;
		//Fill on server
		public ulong playerId;

		public BasePlayerAction() : this(PlayerActionType.None) {

		}

		public BasePlayerAction(PlayerActionType actionType) {
			this.actionType = actionType;
		}

		static public byte OneObjectSize => 1;

		static public byte[] Serialize(BasePlayerAction state) {
			return new byte[] { (byte)state.actionType };
		}

		static public BasePlayerAction Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public BasePlayerAction Deserialize(byte[] bytes);");

			return new BasePlayerAction((PlayerActionType)bytes[0]);
		}
	}
}
