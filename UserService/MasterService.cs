using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

using UserStorage;
using UserStorage.Repository;
using UserService.Interfaces;

namespace UserService
{
    public class MasterService: IRole
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository repository;

        public MasterService(UserRepository rep)
        {
            repository = rep;
        }

        public int Add(User user)
        {
            
            return repository.Add(user);
        }

        public void Delete(User user)
        {
            
            repository.Delete(user);
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            return repository.SearchForUser(criteria);
        }
    }
}
