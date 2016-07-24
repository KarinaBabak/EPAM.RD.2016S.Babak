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
using UserStorage.Service;

namespace UserService
{
    public class SlaveService : IService, IObserver
    {        
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository repository;        
        
        public SlaveService(IUserRepository rep)
        {            
            repository = rep;
        }

        public void Add(User user)
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

        public void Update(IUserRepository repository, MessageService message)
        {
            this.repository = repository;
        }        
    }
}
