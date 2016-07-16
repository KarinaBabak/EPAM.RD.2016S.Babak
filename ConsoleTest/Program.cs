using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserStorage.Repository;
using UserStorage;
using UserService;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository rep = new UserRepository();
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            rep.Add(user);

            MasterService master = new MasterService(rep);
            MasterService master2 = new MasterService(rep);
            master.Add(new User());
            SlaveService slave1 = new SlaveService(rep);
           
            rep.WriteToXML();
            
        }
    }
}
