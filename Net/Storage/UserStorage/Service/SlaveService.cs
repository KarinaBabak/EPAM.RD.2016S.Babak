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
    /// <summary>
    /// Determination of slave service. Service can not add and delete users
    /// </summary>
    public class SlaveService : UserService
    {
        /// <summary>
        /// NLog field
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// BooleanSwitch field for activating logging
        /// </summary>
        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);
       
        /// <summary>
        /// Default constructor
        /// </summary>
        public SlaveService()
        {            
        }

        /// <summary>
        /// Adding new user is enable
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns>InvalidOperation exception</returns>
        public override int Add(User user)
        {
            Logger.Error("Slaves can not add new user");
            throw new InvalidOperationException();            
        }

        /// <summary>
        /// Removing new user is enable
        /// </summary>
        /// <param name="user">user for removing</param>
        public override void Delete(User user)
        {
            Logger.Error("Slaves can not delete user");
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Override adding communicator
        /// </summary>
        /// <param name="communicator">set communicator</param>
        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            Communicator.UserAdded += OnUserAdded;
            Communicator.UserDeleted += OnUserDeleted;
        }

        #region Private Methods
        /// <summary>
        /// Add user to repository, if message from communicator received
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="args">user updated event arguments</param>
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

        /// <summary>
        /// Delete user from repository, if message from communicator received
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="args">user updated event arguments</param>
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
