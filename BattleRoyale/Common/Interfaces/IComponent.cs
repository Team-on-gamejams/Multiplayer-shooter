using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public interface IComponent {
		IGameObject Owner { get; }

		void ProcessMessage(IComponentMessage msg);

		bool CheckDependComponents();
	}
}
