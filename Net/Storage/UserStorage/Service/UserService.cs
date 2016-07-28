using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.Interfaces;


namespace UserStorage.Interfaces
{
    public abstract class UserService: MarshalByRefObject
    {
        public UserRepository Repository { get; set; }

        public UserService(UserRepository rep)
        {
            Repository = rep;            
        }

        public UserService()
        {
            Repository = new UserRepository();
        }

        public void Add(User user) { }
        public void Delete(User user) { }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            return Repository.SearchForUser(criteria);
        }
    }
}
