using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ConfigurationService;
using UserStorage.Interfaces;
using UserStorage;
using NLog;
using System.Reflection;


namespace UserStorage.Interfaces
{
    public static class ServiceInitializer
    {        
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public static UserService Master { get; private set; }
        public static List<UserService> SlavesList = new List<UserService>();//{ get; private set; }
        public static void InitializeServices(UserRepository repository)
        {
            //SlavesList = new List<UserService>();

            var section = (ServiceConfigSection)ConfigurationManager.GetSection("ServiceConfig");

            for (int i = 0; i < section.ServiceItems.Count; i++)
            {
                AppDomain newAppDomain = AppDomain.CreateDomain(section.ServiceItems[i].Login);
                logger.Info("Domain for {0} is created", section.ServiceItems[i].ServiceType);
                var type = typeof(DomainAssemblyLoader);
                var loader = (DomainAssemblyLoader)newAppDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);
                var service = loader.CreateService(section.ServiceItems[i].ServiceType);

                if (section.ServiceItems[i].ServiceType.Contains("Slave"))
                {
                    SlavesList.Add(service);
                }
                else 
                {
                    Master = service;
                }
            }
        }        
    }
}
