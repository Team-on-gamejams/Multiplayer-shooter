using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

using Common;

namespace Network {
	static public class Protocol {
		static public PacketType BaseRecieve(NetworkStream stream, out byte[] data) {
			//Recieve PacketType and bytes size
			PacketType res;
			data = new byte[5];
			stream.Read(data, 0, 5);
			res = (PacketType)data[0];

			//Recieve bytes
			data = new byte[BitConverter.ToInt32(data, 1)];
			stream.Read(data, 0, data.Length);

			return res;
		}

		static public void BaseSend(NetworkStream stream, PacketType type, byte[] data) {
			var a = new byte[5];
			a[0] = (byte)type;
			Array.Copy(BitConverter.GetBytes(data.Length), 0, a, 1, 4);

			//Send PacketType and bytes size
			stream.Write(a, 0, 5);

			//Send bytes
			stream.Write(data, 0, data.Length);
		}
	}
}
