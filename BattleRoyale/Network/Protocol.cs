using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Network {
	static public class Protocol {
		static public void BaseRecieve(NetworkStream stream, out byte[] data) {
			//Recieve bytes size
			data = new byte[4];
			stream.Read(data, 0, 4);

			//Recieve bytes
			data = new byte[BitConverter.ToInt32(data, 0)];
			stream.Read(data, 0, data.Length);
		}

		static public void BaseSend(NetworkStream stream, byte[] data) {
			//Send bytes size
			stream.Write(BitConverter.GetBytes(data.Length), 0, 4);
			//Send bytes
			stream.Write(data, 0, data.Length);
		}


	}
}
