using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.Interfaces;
using ConfigurationService;
using DomainWorker;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserStorage.Interfaces.UserRepository rep = new UserStorage.Interfaces.UserRepository();
            ServiceInitializer.InitializeServices(rep);
           
            //for (int i = 0; i < 100; i++)
            //{
            //    var user = new User("Bogdanovich" + i, "Maxim" + i, new DateTime(1960, 7, 20, 18, 30, 25),
            //    Gender.Male);
            //    MasterService master = (MasterService)ServiceInitializer.Master;
            //    master.Communicator = ServiceInitializer.MasterCommunicator;
            //    master.Add(user);

            //    Console.WriteLine(master.Repository.Users.Count);
            //    foreach (var item in ServiceInitializer.SlavesList)
            //    {
            //        Console.WriteLine(item.Repository.Users.Count);
            //    }
            //    Thread.Sleep(300);               
            //}
            //RunSlaves((IEnumerable<SlaveService>)ServiceInitializer.SlavesList);
            RunMaster((MasterService)ServiceInitializer.Master);
             Console.ReadKey();


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
                        Console.Write(result + " ");
                    Console.WriteLine();
                    Thread.Sleep(rand.Next(1000, 5000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<User>();
                users.Add(new User { FirstName = "Kate", LastName = "Kotova"});
                users.Add(new User { FirstName = "Oxana", LastName = "Sweet"});
                
                User userToDelete = null;

                while (true)
                {
                    foreach (var user in users)
                    {
                        int addChance = rand.Next(0, 3);
                        if (addChance == 0)
                            master.Add(user);

                        Thread.Sleep(rand.Next(1000, 4000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 3);
                            if (deleteChance == 0)
                                userToDelete = user;
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
                            Console.Write(user + " ");
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
