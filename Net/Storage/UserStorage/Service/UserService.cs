using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using NLog;
using UserStorage;
using UserStorage.Interfaces;
using UserStorage.NetworkWorker;
using UserStorage.Service.WCFService;


namespace UserStorage.Interfaces
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant, IncludeExceptionDetailInFaults = true)]

    public abstract class UserService : MarshalByRefObject, IUserServiceContract
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);
        
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

        #region Properties
        public Communicator Communicator { get; set; }
        public UserRepository Repository { get; set; }
        
        public string Name { get; set; } 
        protected ReaderWriterLockSlim ServiceLock
        {
            get;
            set;
        }
        #endregion

        public virtual int Add(User user) 
        {
            return 1;
        }
        public virtual void Delete(User user) 
        { 
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            ServiceLock.EnterReadLock();
            try
            {
                if (BoolSwitch.Enabled)
                    Logger.Trace("SearchForUser is called by service");
                return Repository.SearchForUser(criteria);
            }
            finally
            {
                ServiceLock.ExitReadLock();
            }
        }

        public IEnumerable<int> SearchForUser(ISearchСriterion<User>[] criteria)
        {
            return Repository.SearchForUsers(criteria);
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
