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

		public void SendMessage(ComponentMessageBase msg){
            messageQueue.Enqueue(msg);
        }
		public bool TryReadMessage(out ComponentMessageBase msg){
            return messageQueue.TryDequeue(out msg);
        }

		public BaseComponent[] Components{
            get{
                return components.ToArray();
            }
        }
		public ulong Id { get; private set; }

		public GameObjectType GOType { get; protected set; }
		public BaseGameObject Parent { get; protected set; }

        bool IsDisposed() { 
            return isDisposed;
        }
		public void Dispose() {
			isDisposed = true;
		}

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
