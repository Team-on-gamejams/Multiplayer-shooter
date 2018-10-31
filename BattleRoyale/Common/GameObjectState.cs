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

		static public byte OneObjectSize => /*1 + 8 + 2 * 4 + 2 + 2 * 4*/27 + 8;

		static public byte[] Serialize(GameObjectState state) {
			byte[] bytes = new byte[OneObjectSize];

			bytes[0] = (byte)state.TextureId;
			Array.Copy(BitConverter.GetBytes(state.Id), 0, bytes, 1, 8);

			Array.Copy(BitConverter.GetBytes(state.Pos.x), 0, bytes, 9, 4);
			Array.Copy(BitConverter.GetBytes(state.Pos.y), 0, bytes, 13, 4);

			Array.Copy(BitConverter.GetBytes(state.Angle), 0, bytes, 17, 2);

			Array.Copy(BitConverter.GetBytes(state.Size.width), 0, bytes, 19, 4);
			Array.Copy(BitConverter.GetBytes(state.Size.height), 0, bytes, 23, 4);
			Array.Copy(BitConverter.GetBytes(state.ticks), 0, bytes, 27, 8);

			return bytes;
		}

		static public GameObjectState Deserialize(byte[] bytes) {
			if (bytes.Length != OneObjectSize)
				throw new ApplicationException("Wrong byte[] size in static public GameObjectState Deserialize(byte[] bytes);");

			GameObjectState rez = new GameObjectState();
			rez.TextureId = (TextureId)bytes[0];
			rez.Id = (ulong)BitConverter.ToInt64(bytes, 1);
			rez.Pos.x = (uint)BitConverter.ToInt32(bytes, 9);
			rez.Pos.y = (uint)BitConverter.ToInt32(bytes, 13);
			rez.Angle = BitConverter.ToInt16(bytes, 17);
			rez.Size.width = (uint)BitConverter.ToInt32(bytes, 19);
			rez.Size.height = (uint)BitConverter.ToInt32(bytes, 23);
			rez.ticks = BitConverter.ToInt64(bytes, 27);

			return rez;
		}
	}
}
