using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UserStorage.NetworkWorker
{
    /// <summary>
    /// Class for network communication between sockets
    /// </summary>
    [Serializable]
    public class Communicator : MarshalByRefObject, IDisposable
    {     
        /// <summary>
        /// Sender of message
        /// </summary>
        private Sender sender;

        /// <summary>
        /// Receiver of message
        /// </summary>
        private Receiver receiver;

        /// <summary>
        /// Task for the receiver
        /// </summary>
        private Task recieverTask;

        /// <summary>
        /// Cancellation Token Source
        /// </summary>
        private CancellationTokenSource token;

        #region ctors

        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="receiver">receiver of message</param>
        public Communicator(Sender sender, Receiver receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="sender">sender of message</param>
        public Communicator(Sender sender) : this(sender, null)
        {
        }

        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="receiver">receiver of message</param>
        public Communicator(Receiver receiver)
            : this(null, receiver)
        {
        }
        #endregion

        #region Events
        /// <summary>
        /// event for adding new user
        /// </summary>
        public event EventHandler<DataUpdatedEventArgs> UserAdded;

        /// <summary>
        /// event for removing user
        /// </summary>
        public event EventHandler<DataUpdatedEventArgs> UserDeleted;
        #endregion

        /// <summary>
        /// Connection sender and receivers
        /// </summary>
        /// <param name="endPoints">IP address and port</param>
        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (sender == null)
            {
                return;
            }

            sender.Connect(endPoints);
        }

        /// <summary>
        /// Starting message receiving
        /// </summary>
        public void RunReceiver()
        {
            if (receiver == null)
            {
                return;
            }

            token = new CancellationTokenSource();
            recieverTask = Task.Run((Action)Receive, token.Token);
        }

        /// <summary>
        /// Stop receiving
        /// </summary>
        public void StopReceiver()
        {
            if (token.Token.CanBeCanceled)
            {
                token.Cancel();
            }
        }

        /// <summary>
        /// Sending message, that method add is called
        /// </summary>
        /// <param name="args">user updated event arguments</param>
        public void SendAdd(DataUpdatedEventArgs args)
        {
            if (sender == null)
            {
                return;
            }

            Send(new Message
            {
                UserData = args.User,
                Operation = MethodType.Add
            });
        }

        /// <summary>
        /// Send message that user is removed
        /// </summary>
        /// <param name="args">user updated event arguments</param>
        public void SendDelete(DataUpdatedEventArgs args)
        {
            if (sender == null)
            {
                return;
            }

            Send(new Message
            {
                UserData = args.User,
                Operation = MethodType.Delete
            });
        }

        /// <summary>
        /// IDisposable realization
        /// </summary>
        public void Dispose()
        {
            if (receiver != null)
            {
                receiver.Dispose();
            }

            if (sender != null)
            {
                sender.Dispose();
            }
        }

        #region Private Methods
        /// <summary>
        /// Receiving message
        /// </summary>
        private void Receive()
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var message = receiver.ReceiveMessage();
                if (message == null)
                {
                    return;
                }

                var args = new DataUpdatedEventArgs
                {
                    User = message.UserData
                };

                if (message.Operation == 0)
                {
                    this.OnUserAdded(this, args);
                }
                else
                {
                    this.OnUserDeleted(this, args);
                }
            }
        }

        /// <summary>
        /// Sending message
        /// </summary>
        /// <param name="message">message contains user information and method's type</param>
        private void Send(Message message)
        {
            sender.Send(message);
        }

        /// <summary>
        /// Event for user removing
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="args">user updated event arguments</param>
        private void OnUserDeleted(object sender, DataUpdatedEventArgs args)
        {
            if (UserDeleted != null)
            {
                this.UserDeleted.Invoke(sender, args);
            }
        }

        /// <summary>
        /// Event for user adding
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="args">user updated event arguments</param>
        private void OnUserAdded(object sender, DataUpdatedEventArgs args)
        {
            if (UserAdded != null)
            {
                UserAdded.Invoke(sender, args);
            }
        }
        #endregion
    }
}
