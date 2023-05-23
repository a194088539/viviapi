using System.IO;
using System.Net.Sockets;

namespace MemcachedLib
{
    public class NetworkStreamIgnoreSeek : NetworkStream
    {
        public NetworkStreamIgnoreSeek(Socket socket, FileAccess access, bool ownsSocket)
          : base(socket, access, ownsSocket)
        {
        }

        public NetworkStreamIgnoreSeek(Socket socket, FileAccess access)
          : base(socket, access)
        {
        }

        public NetworkStreamIgnoreSeek(Socket socket, bool ownsSocket)
          : base(socket, ownsSocket)
        {
        }

        public NetworkStreamIgnoreSeek(Socket socket)
          : base(socket)
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0L;
        }
    }
}
