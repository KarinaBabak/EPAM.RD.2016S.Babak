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

namespace UserService
{
    public class MasterService : IRole, IObservable
    {
        private static int CountMaster { get; set; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private IUserRepository repository;
        private List<IObserver> observers;

        public MasterService(UserRepository rep)
        {
            var value = Convert.ToInt32(ConfigurationManager.AppSettings["MastersNumber"]);
            if (CountMaster >= value || CountMaster < 0)
            {
                logger.Error("The count of masters can not be more than {0} and less 1", value);
                throw new InvalidOperationException("The count of masters can not be  more than " +
                    value.ToString());
            }
            CountMaster++;
            repository = rep;
            observers = new List<IObserver>(); ;
        }
              

        public int Add(User user)
        {
            int userId = repository.Add(user);
            NotifyObservers();
            return userId;
        }

        public void Delete(User user)
        {
            repository.Delete(user);
            NotifyObservers();
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            return repository.SearchForUser(criteria);
        }

        #region Observer pattern
        public void RegisterObserver(IObserver o)
        {
            logger.Trace("MasterService.RegisterObserver called");
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            logger.Trace("MasterService.RemoveObserver called");
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            logger.Trace("MasterService.NotifyObservers called");
            foreach (IObserver o in observers)
            {
                o.Update(repository);
            }
        }
        #endregion
    }
}
