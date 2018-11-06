using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public enum PlayerActionType : byte {
		None = ComponentMessageType.None,

		MoveForward = ComponentMessageType.MoveForward,
		MoveBackward = ComponentMessageType.MoveBackward,
		MoveLeft = ComponentMessageType.MoveLeft,
		MoveRight = ComponentMessageType.MoveRight,
		SkillLMB = ComponentMessageType.SkillLMB,
		SkillRMB = ComponentMessageType.SkillRMB,

		PlayerChangeAngle = ComponentMessageType.PlayerChangeAngle,
	}
}
