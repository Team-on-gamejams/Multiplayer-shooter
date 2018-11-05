using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public enum PacketType : byte {
		None,
		ClientConnect,
		ClientConnectResponce,
		ClientDisconnect,
		ClientDisconnectResponce,
		WorldState,
		PlayerAction,
	}
}
