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
using System.Diagnostics;
using ConfigurationService;
using UserStorage.Service;

namespace UserService
{
    public class MasterService : IService, IObservable
    {
        private static int CountMaster { get; set; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();        
        private IUserRepository repository;
        private List<IObserver> observers;

        public MasterService(UserRepository rep)
        {
            CheckAmountOfMasters();
            AppDomain newAppDomain = AppDomain.CreateDomain("Master");
            repository = rep;
            observers = new List<IObserver>();
            InitializeSlaves();
        }
        public void Add(User user)
        {
            NotifyObservers(new MessageService() { UserData = user, Operation = Operation.Add });            
        }

        public void Delete(User user)
        {
            NotifyObservers(new MessageService() { UserData = user, Operation = Operation.Delete});
        }

        #region
        //public int Add(User user)
        //{
        //    int userId = repository.Add(user);
        //    NotifyObservers();
        //    return userId;
        //}

        //public void Delete(User user)
        //{
        //    repository.Delete(user);
        //    NotifyObservers();
        //}
        #endregion
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

        public void NotifyObservers(MessageService message)
        {
            logger.Trace("MasterService.NotifyObservers called");
            foreach (IObserver o in observers)
            {
                o.Update(repository, message);
            }
        }
        #endregion

        private void CheckAmountOfMasters()
        {
            int value = 0;
            var section = (ServiceConfigSection)ConfigurationManager.GetSection("ServiceConfig");

            for (int i = 0; i < section.ServiceItems.Count; i++)
            {
                if (section.ServiceItems[i].ServiceType.Contains("Master"))
                    value++;
            }
            //AppDomain domain = AppDomain.CreateDomain(section.ServiceItems[0].Login.ToString());
            if (CountMaster >= value || CountMaster < 0)
            {
                logger.Error("The count of masters can not be more than {0} and less 1", value.ToString());
                throw new InvalidOperationException("The count of masters can not be more than " +
                    value.ToString());
            }

            CountMaster++;
        }

        private void InitializeSlaves()
        {            
            var section = (ServiceConfigSection)ConfigurationManager.GetSection("ServiceConfig");
            
            for (int i = 0; i < section.ServiceItems.Count; i++)
            {
                if (section.ServiceItems[i].ServiceType.Contains("Slave"))
                {
                    AppDomain newAppDomain = AppDomain.CreateDomain(section.ServiceItems[i].Login);
                    logger.Info("Domain for {0} is created", section.ServiceItems[i].ServiceType);
                    SlaveService slave = new SlaveService(repository);
                    observers.Add(slave);
                    this.RegisterObserver((IObserver)slave);
                }
            }
        }
       
    }
}
