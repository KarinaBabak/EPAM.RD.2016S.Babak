using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.Interfaces;
using System.Configuration;
using ConfigurationService;

using System.Reflection;

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
            MasterService master = (MasterService)ServiceInitializer.Master;
            //var list2 = ServiceInitializer.SlavesList;
            //List<SlaveService> slaves = (List<SlaveService>)
            
            master.Add(user);
            //ServiceInitializer.Master.Add(user);
            master.Repository.WriteToXML();

           
            
            Console.ReadKey();


            
        }
    }
}
