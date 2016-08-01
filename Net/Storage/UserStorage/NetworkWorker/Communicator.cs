using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace UserStorage.NetworkWorker
{
    public class Communicator : MarshalByRefObject
    {     
        private Sender sender;

        private Receiver receiver;

        private Task recieverTask;

        private CancellationTokenSource token;

        public Communicator(Sender sender, Receiver receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        public Communicator(Sender sender) : this(sender, null)
        {
        }

        public Communicator(Receiver receiver)
            : this(null, receiver)
        {
        }

        #region Events
        public event EventHandler<DataUpdatedEventArgs> UserAdded;

        public event EventHandler<DataUpdatedEventArgs> UserDeleted;
        #endregion

        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (sender == null)
            {
                return;
            }

            sender.Connect(endPoints);
        }

        public void RunReceiver()
        {
            if (receiver == null)
            {
                return;
            }

            token = new CancellationTokenSource();
            recieverTask = Task.Run((Action)Receive, token.Token);
        }

        public void StopReceiver()
        {
            if (token.Token.CanBeCanceled)
            {
                token.Cancel();
            }
        }

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

        private void Send(Message message)
        {
            sender.Send(message);
        }

        private void OnUserDeleted(object sender, DataUpdatedEventArgs args)
        {
            if (UserDeleted != null)
            {
                this.UserDeleted.Invoke(sender, args);
            }
        }

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
