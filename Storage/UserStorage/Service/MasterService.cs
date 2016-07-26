using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Configuration;

using UserStorage;
using UserStorage.Interfaces;
using System.Diagnostics;
using ConfigurationService;
using UserStorage.Interfaces.Observer;

namespace UserStorage.Interfaces
{
    public class MasterService : UserService, IObservable
    {
        private static int CountMaster { get; set; }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();      
        
        private List<IObserver> observers;

        public MasterService()
        {
            //CheckAmountOfMasters();                   
            observers = new List<IObserver>();
            
        }
        public new void Add(User user)
        {
            Repository.Add(user);
            NotifyObservers(new MessageService() { UserData = user, Operation = MethodType.Add });            
        }

        public new void Delete(User user)
        {
            Repository.Delete(user);
            NotifyObservers(new MessageService() { UserData = user, Operation = MethodType.Delete});
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
            var l = ServiceInitializer.SlavesList;
            //foreach (var obs in ServiceInitializer.SlavesList)
            //{
            //    observers.Add((IObserver)obs);
            //}
            foreach (IObserver o in observers)
            {
                o.Update(message);
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
                    SlaveService slave = new SlaveService();
                    observers.Add(slave);
                    this.RegisterObserver((IObserver)slave);
                }
            }
        }
       
    }
}
