using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserStorage.Repository;
using UserStorage;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            UserRepository rep = new UserRepository();
            rep.Add(new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male));
            rep.WriteToXML();
            
        }
    }
}
