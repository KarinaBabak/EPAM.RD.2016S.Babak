using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NLog;

using UserStorage;
using UserStorage.Interfaces;
using ConfigurationService;
using UserStorage.NetworkWorker;

namespace UserStorage.Interfaces
{
    public class SlaveService : UserService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly BooleanSwitch boolSwitch = new BooleanSwitch("Switch", string.Empty);

        public SlaveService()
        {            
        }

        public new void Add (User user)
        {
            logger.Error("Slaves can not add new user");
            throw new InvalidOperationException();
        }

        public new void Delete (User user)
        {
            logger.Error("Slaves can not delete user");
            throw new InvalidOperationException();
        }
       
        protected void OnUserAdded (object sender, DataUpdatedEventArgs args)
        {
            try
            {
                if (boolSwitch.Enabled)
                {
                    logger.Info("message");
                }
                ServiceLock.EnterWriteLock();
                Repository.Add(args.User);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }
        }
        protected void OnUserDeleted (object sender, DataUpdatedEventArgs args)
        {
            try
            {
                if (boolSwitch.Enabled)
                {
                    logger.Trace("Slave: OnDeleted is called");
                }
                ServiceLock.EnterWriteLock();
                Repository.Delete(args.User);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }
        }

        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            Communicator.UserAdded += OnUserAdded;
            Communicator.UserDeleted += OnUserDeleted;
        }
    }
}
