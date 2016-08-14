using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ConfigurationService;
using DomainWorker;
using Iterator;
using UserStorage;
using UserStorage.Interfaces;
using System.ServiceModel;
using System.Net;
using System.Net.Sockets;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository rep = new UserRepository();
            ServiceInitializer.InitializeServices(rep);
            var slaves = ServiceInitializer.SlavesList.Where(s => s is SlaveService).Select(s => (SlaveService)s);
            var master = (MasterService)ServiceInitializer.Master;

            for (int i = 0; i < 3; i++)
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

            CreateServiceHosts(services);
            //RunSlaves(slaves);
            //RunMaster(master);
            //Console.ReadKey();
            //while (true)
            //{
            //    var quit = Console.ReadKey();
            //    if (quit.Key == ConsoleKey.Escape)
            //    {
            //        break;
            //    }
            //}
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
        private static void RunMaster(MasterService master)
        {
            Random rand = new Random();

            ThreadStart masterSearch = () =>
            {
                while (true)
                {
                    var serachresult = master.Repository.SearchForUser(u => u.FirstName != null);
                    Console.Write("Master search results: ");
                    foreach (var result in serachresult)
                    {
                        Console.Write(result + " ");
                    }
                    Console.WriteLine();
                    Thread.Sleep(rand.Next(1000, 5000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<User>();
                users.Add(new User { FirstName = "Kate", LastName = "Kotova" });
                users.Add(new User { FirstName = "Oxana", LastName = "Sweet" });

                User userToDelete = null;

                while (true)
                {
                    foreach (var user in users)
                    {
                        int addChance = rand.Next(0, 3);
                        if (addChance == 0)
                        {
                            master.Add(user);
                        }

                        Thread.Sleep(rand.Next(1000, 4000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 3);
                            if (deleteChance == 0)
                            {
                                userToDelete = user;
                            }

                            master.Delete(user);
                        }

                        Thread.Sleep(rand.Next(1000, 5000));
                        Console.WriteLine();
                    }
                }
            };

            Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
            Thread masterAddThread = new Thread(masterAddDelete) { IsBackground = true };
            masterAddThread.Start();
            masterSearchThread.Start();
        }

        private static void RunSlaves(IEnumerable<SlaveService> slaves)
        {
            Random rand = new Random();

            foreach (var slave in slaves)
            {
                var slaveThread = new Thread(() =>
                {
                    while (true)
                    {
                        var userIds = slave.SearchForUser(u => !string.IsNullOrEmpty(u.FirstName));
                        Console.WriteLine("Slave search results: ");
                        foreach (var user in userIds)
                        {
                            Console.Write(user + " ");
                        }

                        Console.WriteLine();
                        Thread.Sleep((int)(rand.NextDouble() * 3000));
                    }
                });

                slaveThread.IsBackground = true;
                slaveThread.Start();
            }
        }
    }
}
