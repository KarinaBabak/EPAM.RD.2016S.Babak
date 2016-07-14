using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Iterator;

namespace UserStorage.Repository
{    
    public class UserRepository: IUserRepository
    {
        private List<User> users;
        private ICustomerIterator iterator;

        public UserRepository()
        {
            users = new List<User>();
            iterator = new CustomIterator();
        }

        /// <summary>
        /// Removing user
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            User userForRemoving = users.FirstOrDefault(u => u.Id == user.Id);
            if (userForRemoving != null)
                users.Remove(userForRemoving);
        }

        public int Add(User user)
        {
            user.Id = iterator.GetNext();
            users.Add(user);
            return user.Id;
        }

        public IEnumerable<User> GetAll()
        {            
            return users;
        }
        #region Search User
        public User GetById(int id)
        {
            return users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByPredicate(Predicate<User> predicate)
        {
            var newUser = users.Find(predicate);
            if (ReferenceEquals(newUser, null))
                return null;
            return newUser;
            
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {            
            List<User> usersFromSearch = users.FindAll(criteria);
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

            using (FileStream fs = new FileStream("Users.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }
        }

        public void ReadFromXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(User));

            using (FileStream fs = new FileStream("Users.xml", FileMode.OpenOrCreate))
            {
                User[] newUsers = (User[])formatter.Deserialize(fs);
                users = newUsers.ToList();
            }
        }

    }
}
