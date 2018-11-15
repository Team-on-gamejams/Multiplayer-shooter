using System;
using System.Windows;
using System.Runtime.InteropServices;

namespace BattleRoyale {
	static class Extensions {
		static public Common.Coord ToCoord(this Point p) {
			return new Common.Coord((uint)p.X, (uint)p.Y);
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocConsole();
	}
}
