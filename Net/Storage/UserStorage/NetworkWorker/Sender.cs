using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace UserStorage.NetworkWorker
{
    /// <summary>
    /// Determination of sender
    /// </summary>
    [Serializable]
    public class Sender : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private List<Socket> sockets = new List<Socket>();

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="ipEndPoints">IP address and port</param>
        public void Connect(IEnumerable<IPEndPoint> ipEndPoints)
        {
            foreach (var ipEndPoint in ipEndPoints)
            {
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                sockets.Add(socket);
            }
        }       

        /// <summary>
        /// Sending a message
        /// </summary>
        /// <param name="message">message contains user information and method's type</param>
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

        /// <summary>
        /// IDisposable realization
        /// </summary>
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
