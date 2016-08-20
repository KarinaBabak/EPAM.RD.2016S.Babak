using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using NLog;
using UserStorage.NetworkWorker;
using UserStorage.Interfaces;
using ConfigurationService;

namespace UserStorage.Interfaces
{
    /// <summary>
    /// Determination of master service
    /// </summary>
    public class MasterService : UserService
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
        public MasterService() 
        {            
        }              

        /// <summary>
        /// Master can add user to repository
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns>id of new user</returns>
        public override int Add(User user)
        {
            ServiceLock.EnterWriteLock();
            int id = 0;
            try
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Trace("Master is adding a new user");
                }

                id = Repository.Add(user);             
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error("Master adds a new user: " + ex.Message);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }
            
            OnUserAdded(this, new DataUpdatedEventArgs() { User = user });
            return id;
        }

        /// <summary>
        /// Override removing user
        /// </summary>
        /// <param name="user">user to remove</param>
        public override void Delete(User user)
        {
            ServiceLock.EnterWriteLock();
            try
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Trace("Master is trying to remove the user");
                }

                Repository.Delete(user);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }

            OnUserDeleted(
                this, 
                new DataUpdatedEventArgs() 
                {  
                    User = user 
                });           
        }

        /// <summary>
        /// Send message that user is added
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="arg">user updated event arguments</param>
        protected virtual void OnUserAdded(object sender, DataUpdatedEventArgs arg)
        {
            if (this.Communicator != null)
            {
                Communicator.SendAdd(arg);                 
            }            
        }

        /// <summary>
        /// Send message that user is removed
        /// </summary>
        /// <param name="sender">sender of message</param>
        /// <param name="arg">user updated event arguments</param>
        protected virtual void OnUserDeleted(object sender, DataUpdatedEventArgs arg)
        {
            if (Communicator != null)
            {
                Communicator.SendDelete(arg);
            }
        }             
    }
}
