using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.NetworkWorker
{
    [Serializable]
    public class Sender : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private List<Socket> sockets = new List<Socket>();

        public void Connect(IEnumerable<IPEndPoint> ipEndPoints)
        {
            foreach (var ipEndPoint in ipEndPoints)
            {
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                sockets.Add(socket);
            }
        }       

        public void Send(Message message)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            foreach (var socket in this.sockets)
            {
                using (var networkStream = new NetworkStream(socket))
                {
                    formatter.Serialize(networkStream, message);
                }
            }

            Logger.Info("Message send!");
        }

        public void Dispose()
        {
            foreach (var socket in this.sockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
