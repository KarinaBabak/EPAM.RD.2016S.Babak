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

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserStorage.Interfaces.UserRepository rep = new UserStorage.Interfaces.UserRepository();
            ServiceInitializer.InitializeServices(rep);
            var user = new User("Bogdanovich", "Maxim", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            //var list = ServiceInitializer.SlavesList;
            //foreach (var item in ServiceInitializer.SlavesList)
            //{
                //Console.WriteLine(item.Repository.Users.Count());
            //}
            MasterService master = (MasterService)ServiceInitializer.Master;
            //var list2 = ServiceInitializer.SlavesList;
            //List<SlaveService> slaves = ServiceInitializer.SlavesList;
            master.Communicator = ServiceInitializer.MasterCommunicator;
            master.Add(user);
           
            Console.WriteLine(master.Repository.Users.Count);
            foreach(var item in ServiceInitializer.SlavesList)
            {
                Console.WriteLine(item.Repository.Users.Count);
            }
            //ServiceInitializer.Master.Add(user);
            //master.Repository.WriteToXML();
           
           
            
            Console.ReadKey();


            
        }
    }
}
