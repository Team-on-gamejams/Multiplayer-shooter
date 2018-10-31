using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public interface IGameObject : IDisposable {
		void SendMessage(IComponentMessage msg);
		bool TryReadMessage(out IComponentMessage msg);

		GameObjectType GOType { get; }
		IGameObject Parent { get; }
		IComponent[] Components { get; }
		ulong Id { get; }

		T GetComponent<T>() where T : class, IComponent;

		void Process();

		bool IsDisposed();
	}
}
