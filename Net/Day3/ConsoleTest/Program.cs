using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attributes;
using System.ComponentModel;
using System.Reflection;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Type userType = typeof(User);
            InstantiateUserAttribute[] instantiateUserAttributes =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));

            User[] users = new User[3];
            for (int i = 0; i < 3; i++)
            {
                users[i] = new User(instantiateUserAttributes[i].Id);
                users[i].FirstName = instantiateUserAttributes[i].ProperyFirstName;
                users[i].LastName = instantiateUserAttributes[i].PropertyLastName;
                Console.WriteLine(users[i].Id.ToString() + ' ' + users[i].FirstName + ' ' + users[i].LastName);
            }

           
            Console.ReadKey();
        }
                       
    }
}
