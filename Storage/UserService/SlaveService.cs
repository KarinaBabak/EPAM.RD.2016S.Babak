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
using ConfigurationService;

namespace UserService
{
    public class SlaveService : IRole, IObserver
    {
        private static int CountSlaves { get; set; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository repository;        
        
        public SlaveService(IUserRepository rep)
        {
            int value = 0;
            var section = (ServiceConfigSection)ConfigurationManager.GetSection("ServiceConfig");            
            if ( section != null )
            {
                value = Convert.ToInt32(section.ServiceItems[1].Number);
            }
            
            //var value = Convert.ToInt32(ConfigurationManager.AppSettings["SlavesNumber"]);
            if (CountSlaves >= value || CountSlaves < 0)
            {
                logger.Error("The count of slaves can not be more than {0}", value);
                throw new InvalidOperationException("The count of slaves can not be more than " +
                    value.ToString());
            }
            
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
