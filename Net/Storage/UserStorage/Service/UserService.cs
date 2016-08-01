using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.Interfaces;
using UserStorage.NetworkWorker;

namespace UserStorage.Interfaces
{
    public abstract class UserService : MarshalByRefObject
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly BooleanSwitch boolSwitch = new BooleanSwitch("Switch", string.Empty);
        public Communicator Communicator { get; set; }
        public UserRepository Repository { get; set; }
        protected ReaderWriterLockSlim ServiceLock 
        { 
            get; 
            set; 
        }   
 
        public UserService(UserRepository rep)
        {
            Repository = rep;
            ServiceLock = new ReaderWriterLockSlim();  
        }

        public UserService()
        {
            Repository = new UserRepository();
            ServiceLock = new ReaderWriterLockSlim();  
        }

        public void Add(User user) 
        { 
        }
        public void Delete(User user) 
        { 
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            ServiceLock.EnterReadLock();
            try
            {
                if (boolSwitch.Enabled)
                    logger.Trace("SearchForUser is called by service");
                return Repository.SearchForUser(criteria);
            }
            finally
            {
                ServiceLock.ExitReadLock();
            }
        }

        public virtual void AddCommunicator(Communicator communicator)
        {
            if (communicator == null)
            {
                return;
            }

            this.Communicator = communicator;
        }
    }
}
