using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Interfaces;
using System.ServiceModel;
using System.ServiceModel.Description;
using DomainWorker;
using UserStorage;
using UserStorage.Service.WCFService;
using ClientService.ServiceReference1;

namespace ClientService
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User("Bogdanovich", "Maxim", new DateTime(1891, 12, 9, 18, 30, 25), Gender.Male);
            UserServiceContractClient client = new UserServiceContractClient();
            client.Open();

            client.Add(user);
            
            client.Close();
            Console.ReadKey();
        }

       
    }
}
