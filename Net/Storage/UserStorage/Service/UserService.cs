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
    /// <summary>
    /// Abstract class for user service
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant, IncludeExceptionDetailInFaults = true)]
    public abstract class UserService : MarshalByRefObject, IUserServiceContract
    {
        /// <summary>
        /// NLog field
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// BooleanSwitch field for activating logging
        /// </summary>
        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);

        #region ctor
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="rep">repository for managing by services</param>
        public UserService(UserRepository rep)
        {
            Repository = rep;
            ServiceLock = new ReaderWriterLockSlim();  
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserService()
        {
            Repository = new UserRepository();
            ServiceLock = new ReaderWriterLockSlim();  
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets communication between services
        /// </summary>
        public Communicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets Repository for managing by services
        /// </summary>
        public UserRepository Repository { get; set; }
        
        /// <summary>
        /// Gets or setsLogin of service
        /// </summary>
        public string Name { get; set; } 

        /// <summary>
        ///  Gets or sets sync
        /// </summary>
        protected ReaderWriterLockSlim ServiceLock
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// Adding a new user
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns>id of new user</returns>
        public virtual int Add(User user) 
        {
            return 1;
        }

        /// <summary>
        /// Removing of user
        /// </summary>
        /// <param name="user">user for removing</param>
        public virtual void Delete(User user) 
        { 
        }
        
        /// <summary>
        /// Searching by criterion
        /// </summary>
        /// <param name="criterion">criterion for Searching</param>
        /// <returns>list with users id</returns>
        public List<int> SearchForUser(Func<User, bool> criterion)     
        {
            ServiceLock.EnterReadLock();
            try
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Trace("SearchForUser is called by service");
                }

                return Repository.SearchForUser(criterion);
            }
            finally
            {
                ServiceLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Search user by criteria
        /// </summary>
        /// <param name="criteria">criteria for searching</param>
        /// <returns>users id</returns>
        public IEnumerable<int> SearchForUser(ISearchСriterion<User>[] criteria)
        {
            return Repository.SearchForUsers(criteria);
        }

        /// <summary>
        /// Add communicator for communication between services
        /// </summary>
        /// <param name="communicator">communicator for setting</param>
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
