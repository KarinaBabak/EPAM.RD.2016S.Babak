
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using NLog;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Interfaces;
using ConfigurationService;
using UserStorage.NetworkWorker;

namespace DomainWorker
{
    public class ServiceInitializer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static UserService Master { get; private set; }

        public static List<UserService> SlavesList { get; private set; }

        public static Communicator MasterCommunicator { get; private set; }          

        public static void InitializeServices(UserRepository repository)
        {            
            SlavesList = new List<UserService>();            
            List<IPEndPoint> slavesIPEndPoints = new List<IPEndPoint>();

            var section = (ServiceConfigSection)ConfigurationManager.GetSection("ServiceConfig");
            Receiver receiver = null;
            
            for (int i = 0; i < section.ServiceItems.Count; i++)
            {
                AppDomain newAppDomain = AppDomain.CreateDomain(section.ServiceItems[i].Login);
                logger.Info("Domain for {0} is created", section.ServiceItems[i].ServiceType);
                var type = typeof(DomainAssemblyLoader);
                var loader = (DomainAssemblyLoader)newAppDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);
                var service = loader.CreateService(section.ServiceItems[i].ServiceType);

                if (section.ServiceItems[i].ServiceType.Contains("Slave"))
                {                    
                    try
                    {
                        SlavesList.Add(service);
                        receiver = new Receiver(IPAddress.Parse(section.ServiceItems[i].Address), Int32.Parse(section.ServiceItems[i].Port));
                        var communicator = new Communicator(receiver);                                                
                        service.AddCommunicator(communicator);
                        Task task = receiver.AcceptConnection();
                        service.Communicator.RunReceiver();
                        slavesIPEndPoints.Add(new IPEndPoint(IPAddress.Parse(section.ServiceItems[i].Address), Int32.Parse(section.ServiceItems[i].Port)));
                     }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
                else
                {                                   
                    MasterCommunicator = new Communicator(new Sender());                    
                    Master = service;     
                    Master.AddCommunicator(MasterCommunicator);
                }
            }

            MasterCommunicator.Connect(slavesIPEndPoints);

            foreach (UserService service in SlavesList)
            {
                service.Communicator.RunReceiver();
            }
        }        
    }
}
