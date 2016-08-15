using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using ClientService.ServiceReference1;


namespace ClientService
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User
            {
                FirstNamek__BackingField = "Ivan",
                LastNamek__BackingField = "Ivanov",
                DateOfBirthk__BackingField = DateTime.Now,
                UserGenderk__BackingField = Gender.Male
            };


            try
            {
                UserServiceContractClient client = new UserServiceContractClient("BasicHttpBinding_IUserServiceContract");
                client.Open();

                user.Idk__BackingField = client.Add(user); 
                client.Delete(user);

                MaleCriterion criteria = new MaleCriterion();
                var users = client.SearchForUser(new[] { new MaleCriterion() });
                Console.WriteLine("Serch result: ");
                foreach (var oneUser in users)
                {
                    Console.Write(oneUser + " ");
                }

                client.Close();
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);

            }
            Console.ReadKey();
        }

    }
}
