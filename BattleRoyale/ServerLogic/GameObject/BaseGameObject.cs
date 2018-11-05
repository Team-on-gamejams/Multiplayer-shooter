using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;
using ServerLogic.Components;

namespace ServerLogic.GameObject {
	class BaseGameObject : Common.IGameObject, IDisposable {
		static ulong lastId = 0;

		public void SendMessage(IComponentMessage msg) => messageQueue.Enqueue(msg);
		public bool TryReadMessage(out IComponentMessage msg) => messageQueue.TryDequeue(out msg);

		public IComponent[] Components => components.ToArray();
		public ulong Id { get; private set; }

		public T GetComponent<T>() where T : class, IComponent {
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
				if (messageQueue.TryDequeue(out IComponentMessage msg))
					foreach (var component in components)
						component.ProcessMessage(msg);
			}
		}

		public GameObjectType GOType { get; protected set; }
		public IGameObject Parent { get; protected set; }

		public bool IsDisposed() => isDisposed;
		public void Dispose() => isDisposed = true;

		protected readonly List<IComponent> components;

		readonly ConcurrentQueue<IComponentMessage> messageQueue;
		bool isDisposed;

		protected BaseGameObject(GameObjectType goType, BaseGameObject parent) {
			messageQueue = new ConcurrentQueue<IComponentMessage>();
			components = new List<IComponent>();
			Id = ++lastId;
			isDisposed = false;

			this.GOType = goType;
			this.Parent = parent;
		}
	}
}
