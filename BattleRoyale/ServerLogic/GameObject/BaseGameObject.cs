using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServerLogic.Components;

namespace ServerLogic.GameObject {
	class BaseGameObject : IDisposable {
		static ulong lastId = 0;

		public void SendMessage(ComponentMessageBase msg) => messageQueue.Enqueue(msg);
		public bool TryReadMessage(out ComponentMessageBase msg) => messageQueue.TryDequeue(out msg);

		public BaseComponent[] Components => components.ToArray();
		public ulong Id { get; private set; }

		public T GetComponent<T>() where T : BaseComponent {
			foreach (var c in components)
				if (c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)))
					return c as T;
			return null;
		}

		public void Process() {
			if (IsDisposed())
				return;

			SendMessage(new ComponentMessageBase() {
				ComponentMessageType = ComponentMessageType.TickElapsed
			});

			while (messageQueue.Count > 0) {
				if (messageQueue.TryDequeue(out ComponentMessageBase msg))
					foreach (var component in components)
						component.ProcessMessage(msg);
			}
		}

		public GameObjectType GOType { get; protected set; }
		public BaseGameObject Parent { get; protected set; }

		public bool IsDisposed() => isDisposed;
		public void Dispose() => isDisposed = true;

		protected List<BaseComponent> components;

		GameObjectType goType;
		BaseGameObject parent;
		ConcurrentQueue<ComponentMessageBase> messageQueue;
		ulong id;
		bool isDisposed;

		protected BaseGameObject(GameObjectType goType, BaseGameObject parent) {
			messageQueue = new ConcurrentQueue<ComponentMessageBase>();
			components = new List<BaseComponent>();
			id = ++lastId;
			isDisposed = false;

			this.goType = goType;
			this.parent = parent;

		}
	}
}
