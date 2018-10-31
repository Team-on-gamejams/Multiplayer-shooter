using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class BasePlayerAction {
		public PlayerActionType actionType;

		public BasePlayerAction() : this(PlayerActionType.None) {

		}

		public BasePlayerAction(PlayerActionType actionType) {
			this.actionType = actionType;
		}
	}
}
