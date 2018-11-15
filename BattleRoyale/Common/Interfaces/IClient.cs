using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public interface IClient {
		ulong PlayerId { get; set; }

		void Connect(string ip, ushort port);
		void Disconnect();

		event Action<GameObjectState[]> OnWorldUpdate;

		void SentPlayerAction(BasePlayerAction playerAction);
	}
}
