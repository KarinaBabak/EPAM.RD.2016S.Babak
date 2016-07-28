using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkWorker
{
    [Serializable]
    public class Receiver : IDisposable
    {
        private Socket listener;
        private Socket reciever;
        public IPEndPoint IpPoint { get; private set; }

        public Receiver(string address, string port)
        {
            IpPoint = new IPEndPoint(IPAddress.Parse(address), Int32.Parse(port));
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(IpPoint);
            listener.Listen(1);
        }

        public void ReceiveMessage()
        {

        }

        public void Dispose()
        {
            reciever.Close();
            listener.Close();
        }
    }
}
