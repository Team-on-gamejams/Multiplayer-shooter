using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public interface IClient {
		void Connect(string ip, ushort port);
		void Disconnect();

		void SentPlayerAction(BasePlayerAction playerAction);
		GameObjectState[] GetWorldState();
	}
}
