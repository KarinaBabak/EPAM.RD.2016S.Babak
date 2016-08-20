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
    /// Receiver class
    /// </summary>
    [Serializable]
    public class Receiver : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private Socket listener;
        private Socket reciever;        

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">value of port</param>
        public Receiver(IPAddress address, int port)
        {
            this.IpPoint = new IPEndPoint(address, port);
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(IpPoint);
            this.listener.Listen(1);
        }

        /// <summary>
        /// Gets IP address
        /// </summary>
        public IPEndPoint IpPoint { get; private set; }

        /// <summary>
        /// Determination to accept connection
        /// </summary>
        /// <returns>Task for receiver</returns>
        public Task AcceptConnection()
        {
            return Task.Run(() =>
            {
                Logger.Info("Wait Connection");                
                reciever = listener.Accept();
                Logger.Info("Connection accepted");
            });
        }

        /// <summary>
        /// Receive message
        /// </summary>
        /// <returns>object of message</returns>
        public Message ReceiveMessage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Message message;
           
            using (var networkStream = new NetworkStream(reciever, false))
            {
                message = (Message)formatter.Deserialize(networkStream);
            }

            Console.WriteLine("Message received!");
            return message;
        }

        /// <summary>
        /// IDisposable realization
        /// </summary>
        public void Dispose()
        {
            this.reciever.Close();
            this.listener.Close();
        }
    }
}
