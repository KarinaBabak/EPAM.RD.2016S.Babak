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
    [ServiceContract]
    [ServiceKnownType(typeof(FemaleCriterion))]
    [ServiceKnownType(typeof(MaleCriterion))]
    public interface IUserServiceContract
    {
        [OperationContract]
        int Add(User user);

        [OperationContract]
        void Delete(User user);

        [OperationContract]
        IEnumerable<int> SearchForUser(ISearchСriterion<User>[] criteria);

    }
}
