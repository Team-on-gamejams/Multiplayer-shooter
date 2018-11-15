using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public enum ComponentMessageType : byte{
		None,

		TickElapsed,

		MoveForward,
		MoveBackward,
		MoveLeft,
		MoveRight,
		SkillLMB,
		SkillRMB,

		TakeDmg,

		AngleChanged,

		Collide,

		Die,
	}
}
