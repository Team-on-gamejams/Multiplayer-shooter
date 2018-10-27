using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces {
	public interface IGameObject : IDisposable {
		void SendMessage(ComponentMessageType msg);

		ulong GetId();
		GameObjectState GetState();
		IComponent[] GetComponents();
		GameObjectType GetGOType();
		IGameObject GetParent();

		void Dispose();
		bool IsDDisposed();
	}
}
