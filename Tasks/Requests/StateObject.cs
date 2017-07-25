using System.Net.Sockets;
using System.Text;

namespace Requests
{
    class StateObject
    {
        public Socket workSocket = null;
        public byte[] buffer = new byte[1024];
        public StringBuilder sb = new StringBuilder();
    }
}
