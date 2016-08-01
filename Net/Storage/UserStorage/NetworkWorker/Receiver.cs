﻿using System;
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
    [Serializable]
    public class Receiver : IDisposable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private Socket listener;
        private Socket reciever;
        public IPEndPoint IpPoint { get; private set; }

        public Receiver(IPAddress address, int port)
        {
            this.IpPoint = new IPEndPoint(address, port);
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(IpPoint);
            this.listener.Listen(1);
        }

        public Task AcceptConnection()
        {
            return Task.Run(() =>
            {
                logger.Info("Wait Connection");                
                reciever = listener.Accept();
                logger.Info("Connection accepted");
            });
        }

        public Message ReceiveMessage()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Message message;

            if(reciever == null)
            {
                return null;
            }

            using (var networkStream = new NetworkStream(reciever, false))
            {
                message = (Message)formatter.Deserialize(networkStream);
            }
            Console.WriteLine("Message received!");
            return null;
        }

        public void Dispose()
        {
            this.reciever.Close();
            this.listener.Close();
        }
    }
}
