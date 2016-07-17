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
            InstantiateUserAttribute[] instantiateUserAttribute =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));
            MatchParameterWithPropertyAttribute[] matchParameterWithPropertyAttribute =
                (MatchParameterWithPropertyAttribute[])
                    Attribute.GetCustomAttributes(userType.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));

            List<User> users = new List<User>();

            // Gets the attributes for the property.
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(userType)[matchParameterWithPropertyAttribute[0].Value1].Attributes;

            /* Prints the default value by retrieving the DefaultValueAttribute 
             * from the AttributeCollection. */
            DefaultValueAttribute defIdAttribute =
               (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
            //Console.WriteLine("The default value of ID is: " + defIdAttribute.Value.ToString());

            for (int i = 0; i < 3; i++)
            {

                if (instantiateUserAttribute[i].Id != 0)
                    users.Add(new User(instantiateUserAttribute[i].Id) { FirstName = instantiateUserAttribute[i].PropertyLastName, LastName = instantiateUserAttribute[i].PropertyLastName });
                else
                    users.Add(new User((int)defIdAttribute.Value) { FirstName = instantiateUserAttribute[i].ProperyFirstName, LastName = instantiateUserAttribute[i].PropertyLastName });
                try
                {
                    IsValid(users[i]);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            foreach (var user in users)
            {
                Console.WriteLine("User ID:" + user.Id);
                Console.WriteLine("First name:" + user.FirstName);
                Console.WriteLine("Last name:" + user.LastName);
                Console.WriteLine("-----------------------------");
            }

            Console.ReadKey();
        }

        public static void IsValid(User user)
        {
            StringBuilder exception = new StringBuilder();
            Type userType = typeof(User);
            
            StringValidatorAttribute[] strFirstValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("FirstName"), typeof(StringValidatorAttribute));
            if (user.FirstName.Length > strFirstValid[0].Length) exception.Append("First Name can't be longer then {0} symbols" + strFirstValid[0].Length);
            StringValidatorAttribute[] strLastValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("LastName"), typeof(StringValidatorAttribute));
            if (user.LastName.Length > strLastValid[0].Length) exception.Append("Last Name can't be longer then {0} symbols" + strLastValid[0].Length);
        }
    }
}
