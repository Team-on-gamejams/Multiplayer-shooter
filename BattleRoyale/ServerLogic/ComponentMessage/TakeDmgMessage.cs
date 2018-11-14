using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.ComponentMessage {
	class TakeDmgMessage : ComponentMessageBase{
		public bool CanKill { get; private set; }
		public short Dmg { get; private set; }

		public TakeDmgMessage() : this(true, 0){

		}

		public TakeDmgMessage(bool canKill, short dmg) {
			CanKill = canKill;
			Dmg = dmg;
		}
	}
}
