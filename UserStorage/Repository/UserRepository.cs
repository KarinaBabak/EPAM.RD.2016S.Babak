using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using Iterator;
using UserStorage.Validator;

namespace UserStorage.Repository
{    
    public class UserRepository: IUserRepository
    {
        private List<User> Users { get; set; }
        private ICustomerIterator iterator;
        private UserValidator validator;

        public UserRepository()
        {
            Users = new List<User>();
            iterator = new CustomIterator();
            validator = new UserValidator();
        }

        /// <summary>
        /// Removing user
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            User userForRemoving = Users.FirstOrDefault(u => u.Id == user.Id);
            if (userForRemoving != null)
                Users.Remove(userForRemoving);
        }

        public int Add(User user)
        {
            if (!validator.Validate(user))
            {
                throw new ArgumentException("The age is wrong");
            }
            if (Users.Contains(user))
            {
                throw new InvalidOperationException("User already exist");
            }
            user.Id = iterator.GetNext();
            Users.Add(user);
            return user.Id;
        }

        public IEnumerable<User> GetAll()
        {            
            return Users;
        }
        #region Search User
        public User GetById(int id)
        {
            return Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByPredicate(Predicate<User> predicate)
        {
            var newUser = Users.Find(predicate);
            if (ReferenceEquals(newUser, null))
                return null;
            return newUser;
            
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {            
            List<User> usersFromSearch = Users.FindAll(criteria);
            if (usersFromSearch == null) return null;
            int[] usersID = new int[usersFromSearch.Count];
            for(int i = 0; i < usersID.Length; i++)
            {
                usersID[i] = usersFromSearch[i].Id;
            }
            return usersID;

        }
        #endregion

        public void WriteToXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationManager.AppSettings["xmlPath"];
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }

        public void ReadFromXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationManager.AppSettings["xmlPath"];

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                List<User> newUsers = (List<User>)formatter.Deserialize(fs);
                Users = newUsers;
            }
        }

    }
}
