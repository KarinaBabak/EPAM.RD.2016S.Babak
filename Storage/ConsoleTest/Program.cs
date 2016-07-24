using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserService;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserStorage.Repository.UserRepository rep = new UserStorage.Repository.UserRepository();
            rep.Add(new User("Bogdanovich", "Maxim", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male));
            rep.WriteToXML();

            
        }
    }
}
