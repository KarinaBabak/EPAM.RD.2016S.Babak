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
    public class MasterService : UserService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly BooleanSwitch boolSwitch = new BooleanSwitch("Switch", string.Empty);
              
        
        public MasterService() 
        {  
          
        }

        public new void Add(User user)
        {
            ServiceLock.EnterWriteLock();
            try
            {
                if (boolSwitch.Enabled)
                {
                    logger.Trace("Master is adding a new user");
                }

                Repository.Add(user);             
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }

            OnUserAdded(this, new DataUpdatedEventArgs() { User = user });        
        }

        public new void Delete(User user)
        {
            ServiceLock.EnterWriteLock();
            try
            {
                if (boolSwitch.Enabled)
                {
                    logger.Trace("Master is trying to remove the user");
                }

                Repository.Delete(user);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }

            OnUserDeleted(this, new DataUpdatedEventArgs() {  User = user });           
        }

        protected virtual void OnUserAdded(object sender, DataUpdatedEventArgs arg)
        {
            if(this.Communicator != null)
            {
                Communicator.SendAdd(arg);                 
            }            
        }

        protected virtual void OnUserDeleted(object sender, DataUpdatedEventArgs arg)
        {
            if (Communicator != null)
            {
                Communicator.SendDelete(arg);
            }
        }
             
    }
}
