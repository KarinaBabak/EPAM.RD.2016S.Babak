using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using UserStorage.Interfaces;
using UserStorage.SearchCriteria;

namespace UserStorage.Service.WCFService
{
    /// <summary>
    /// Contract for WCF
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(typeof(FemaleCriterion))]
    [ServiceKnownType(typeof(MaleCriterion))]
    public interface IUserServiceContract
    {
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns>id of new user</returns>
        [OperationContract]
        int Add(User user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user">user for removing</param>
        [OperationContract]
        void Delete(User user);

        /// <summary>
        /// Search users by criteria
        /// </summary>
        /// <param name="criteria">criteria for searching</param>
        /// <returns>users id</returns>
        [OperationContract]
        IEnumerable<int> SearchForUser(ISearchСriterion<User>[] criteria);
    }
}
