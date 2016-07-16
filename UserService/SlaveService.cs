using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Configuration;

using UserStorage;
using UserStorage.Repository;
using UserService.Interfaces;
using UserService.Observer;

namespace UserService
{
    public class SlaveService : IRole, IObserver
    {
        private static int CountSlaves { get; set; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository repository;
        private IObservable master;
        
        public SlaveService(IUserRepository rep)
        {
            var value = Convert.ToInt32(ConfigurationManager.AppSettings["SlavesNumber"]);
            if (CountSlaves > value)
            {
                logger.Error("The count of slaves can not be more than {0}", value);
                throw new ArgumentException("The count of slaves can not be more than {0}",
                    value.ToString());
            }
            master.RegisterObserver(this);
            CountSlaves++;
            repository = rep;
        }

        public int Add(User user)
        {
            logger.Error("Slaves can not add new user");
            throw new InvalidOperationException();
        }

        public void Delete(User user)
        {
            logger.Error("Slaves can not delete user");
            throw new InvalidOperationException();
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            return repository.SearchForUser(criteria);
        }

        public void Update(IUserRepository repository)
        {
            this.repository = repository;
        }
    }
}
