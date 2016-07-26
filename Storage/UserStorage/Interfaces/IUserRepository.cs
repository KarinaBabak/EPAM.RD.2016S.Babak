using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {        
        User GetById(int id);
        User GetUserByPredicate(Predicate<User> predicate);
        IEnumerable<User> GetAll();
        IEnumerable<int> SearchForUser(Predicate<User> criteria);
    }
}
