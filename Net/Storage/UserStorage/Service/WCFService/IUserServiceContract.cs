using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace UserStorage.Service.WCFService
{
    [ServiceContract]
    public interface IUserServiceContract
    {
        [OperationContract]
        void Add(User user);

        [OperationContract]
        void Delete(User user);

        [OperationContract]
        IEnumerable<int> SearchForUser(Predicate<User> criteria);

    }
}
