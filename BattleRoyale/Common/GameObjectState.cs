using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
	public class GameObjectState {
		public TextureId TextureId { get; set; }
		public ulong Id { get; set; }
		public Coord Pos { get; set; }
		public short Angle { get; set; }
		public Size Size { get; set; }
		public long ticks;

		public GameObjectState() {
			Pos = new Coord();
			Size = new Size();
			ticks = DateTime.Now.Ticks;
		}

		public GameObjectState(TextureId textureId, ulong id, Coord pos, short angle, Size size) {
			TextureId = textureId;
			Id = id;
			Pos = pos;
			Angle = angle;
			Size = size;
			ticks = DateTime.Now.Ticks;
		}

		static public byte OneObjectSize => 43 + 8;

		static public byte[] Serialize(GameObjectState state) {
			byte[] bytes = new byte[/*1 + 8 + 2 * 8 + 2 + 2 * 8*/43 + 8];

			bytes[0] = (byte)state.TextureId;
			Array.Copy(BitConverter.GetBytes(state.Id), 0, bytes, 1, 8);

			Array.Copy(BitConverter.GetBytes(state.Pos.x), 0, bytes, 9, 8);
			Array.Copy(BitConverter.GetBytes(state.Pos.y), 0, bytes, 17, 8);

			Array.Copy(BitConverter.GetBytes(state.Angle), 0, bytes, 25, 2);

			Array.Copy(BitConverter.GetBytes(state.Size.width), 0, bytes, 27, 8);
			Array.Copy(BitConverter.GetBytes(state.Size.height), 0, bytes, 35, 8);
			Array.Copy(BitConverter.GetBytes(state.ticks), 0, bytes, 43, 8);

			return bytes;
		}

		static public GameObjectState Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public GameObjectState Deserialize(byte[] bytes);");

			GameObjectState rez = new GameObjectState();
			rez.TextureId = (TextureId)bytes[0];
			rez.Id = (ulong)BitConverter.ToInt64(bytes, 1);
			rez.Pos.x = BitConverter.ToDouble(bytes, 9);
			rez.Pos.y = BitConverter.ToInt64(bytes, 17);
			rez.Angle = BitConverter.ToInt16(bytes, 25);
			rez.Size.width = BitConverter.ToDouble(bytes, 27);
			rez.Size.height = BitConverter.ToDouble(bytes, 35);
			rez.ticks = BitConverter.ToInt64(bytes, 43);

			return rez;
		}
	}
}
