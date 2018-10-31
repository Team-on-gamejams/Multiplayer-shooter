using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public enum PlayerActionType : byte {
		None,
		MoveForward,
		MoveBackward,
		MoveLeft,
		MoveRight,
		SkillLMB,
		SkillRMB
	}
}
