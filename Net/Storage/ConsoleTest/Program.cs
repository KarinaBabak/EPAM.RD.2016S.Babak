using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfigurationService;
using DomainWorker;
using Iterator;
using UserStorage;
using UserStorage.Interfaces;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {            
            ServiceInitializer.InitializeServices();

            var slaves = ServiceInitializer.SlavesList.Where(s => s is SlaveService).Select(s => (SlaveService)s);
            var master = (MasterService)ServiceInitializer.Master;

            for (int i = 0; i < 10; i++)
            {
                var user = new User("Bogdanovich" + i.ToString(), "Maxim" + i.ToString(), new DateTime(1891, 12, 9, 18, 30, 25), Gender.Male);
                master.Communicator = ServiceInitializer.MasterCommunicator;
                master.Add(user);

                Console.WriteLine(master.Repository.Users.Count);
                foreach (var item in ServiceInitializer.SlavesList)
                {
                    Console.WriteLine(item.Repository.Users.Count);
                }

                Thread.Sleep(300);
            }

            List<UserService> services = new List<UserService>();
            services.Add(ServiceInitializer.Master);
            services.AddRange(ServiceInitializer.SlavesList);

            //CreateServiceHosts(services);            
            Console.ReadKey();
        }

        private static void CreateServiceHosts(List<UserService> services)
        {          
            foreach (var service in services)
            {
                Uri serviceUri = new Uri("http://127.0.0.1:8080/Service/" + service.Name);
                ServiceHost host = new ServiceHost(service, serviceUri);
                host.Open();

                foreach (Uri uri in host.BaseAddresses)
                {
                    Console.WriteLine("\t{0}", uri.ToString());
                }

                Console.WriteLine();
                Console.WriteLine("Number of dispatchers listening : {0}", host.ChannelDispatchers.Count);
                foreach (System.ServiceModel.Dispatcher.ChannelDispatcher dispatcher in host.ChannelDispatchers)
                {
                    Console.WriteLine("\t{0}, {1}", dispatcher.Listener.Uri.ToString(), dispatcher.BindingName);
                }

                Console.WriteLine();
            }
        }       
    }
}
