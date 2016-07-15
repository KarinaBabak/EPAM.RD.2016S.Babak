using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserStorage;
using UserStorage.Repository;

namespace UserService.Interfaces
{
    public interface IRole    
    {
        int Add(User user);
        void Delete(User user);
        IEnumerable<int> SearchForUser(Predicate<User> criteria);
       
    }
}
