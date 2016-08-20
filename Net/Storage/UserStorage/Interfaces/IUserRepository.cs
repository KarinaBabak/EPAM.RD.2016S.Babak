using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Interfaces
{
    /// <summary>
    /// Base functionality for user repository
    /// </summary>    
    public interface IUserRepository : IRepository<User>
    {        
        /// <summary>
        /// Getting user by id
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>object of User</returns>
        User GetById(int id);

        /// <summary>
        /// Getting all users
        /// </summary>
        /// <returns>collection with all users</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Search entities by criterion
        /// </summary>
        /// <param name="criteria">criterion for search</param>
        /// <returns>users id</returns>
        List<int> SearchForUser(Func<User, bool> criteria);       

        /// <summary>
        /// Search entities by criteria
        /// </summary>
        /// <param name="criteria">criteria for search</param>
        /// <returns>users id</returns>
        IEnumerable<int> SearchForUsers(ISearchСriterion<User>[] criteria);
    }
}
