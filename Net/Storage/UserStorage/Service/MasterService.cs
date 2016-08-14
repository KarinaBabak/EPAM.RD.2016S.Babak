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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);       
                

        public MasterService() 
        {            
        }
              

        public override void Add(User user)
        {
            ServiceLock.EnterWriteLock();
            try
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Trace("Master is adding a new user");
                }

                Repository.Add(user);             
            }
            catch(InvalidOperationException ex)
            {
                Logger.Error("Master adds a new user: " + ex.Message);
            }
            finally
            {
                ServiceLock.ExitWriteLock();
            }

            OnUserAdded(this, new DataUpdatedEventArgs() { User = user });        
        }

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

            OnUserDeleted(this, new DataUpdatedEventArgs() 
            {  
                User = user 
            });           
        }

        protected virtual void OnUserAdded(object sender, DataUpdatedEventArgs arg)
        {
            if (this.Communicator != null)
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
