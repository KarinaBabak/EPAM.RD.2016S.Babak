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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);

        public SlaveService()
        {
        }

        public new void Add(User user)
        {
            Logger.Error("Slaves can not add new user");
            throw new InvalidOperationException();
        }

        public new void Delete(User user)
        {
            Logger.Error("Slaves can not delete user");
            throw new InvalidOperationException();
        }

        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            Communicator.UserAdded += OnUserAdded;
            Communicator.UserDeleted += OnUserDeleted;
        }

        #region Private Methods
        private void OnUserAdded(object sender, DataUpdatedEventArgs args)
        {
            try
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Info("message");
                }
                ServiceLock.EnterWriteLock();
                Repository.Add(args.User);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }
        }
        private void OnUserDeleted(object sender, DataUpdatedEventArgs args)
        {
            try
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Trace("Slave: OnDeleted is called");
                }
                ServiceLock.EnterWriteLock();
                Repository.Delete(args.User);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }
        }
        #endregion
    }
}
