using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using NLog;
using Iterator;
using UserStorage.Validator;

using UserStorage.Interfaces;

namespace UserStorage.Interfaces
{
    [Serializable]
    public class UserRepository : IUserRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);
                      
        private ICustomIterator iterator;
        
        private UserValidator validator;

        public List<User> Users { get; private set; }        

        public UserRepository(ICustomIterator generator = null, UserValidator validator = null)
        {            
            Users = new List<User>();
            this.iterator =  generator ?? new CustomIterator();
            this.validator = validator ?? new UserValidator();            
        }

        public UserRepository()
        {
            Users = new List<User>();
            iterator = new CustomIterator();
            validator = new UserValidator();            
        }

                
        public void Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            User userForRemoving = Users.FirstOrDefault(u => u.Id == user.Id);
            if (userForRemoving != null)
            {                
                Users.Remove(userForRemoving);                
            }
            else
                throw new ArgumentException("The user is not exist");
        }

        public int Add(User user)
        {
            Logger.Trace("UserRepository.Add called. Create the user: " + user.ToString());            
           
            if (!validator.Validate(user))
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Error("The validation of new user is failed");
                }
                throw new ArgumentException("The validation is failed");
            }
            if (Users.Contains(user))
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Error("The user already exists");
                }
                throw new InvalidOperationException("User already exists");
            }

            user.Id = iterator.GetNext();            
            Users.Add(user);
            return user.Id;
        }

        public IEnumerable<User> GetAll()
        {
            Logger.Trace("UserRepository.GetAll called");           
            return Users;
        }

        public void Clear()
        {
            Logger.Trace("UserRepository.Clear called");
            Users.Clear();
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
            {
                return null;
            }
            return newUser;
        }

        public IEnumerable<int> SearchForUser(Predicate<User> criteria)
        {
            List<User> usersFromSearch = Users.FindAll(criteria);
            if (usersFromSearch == null) return null;
            int[] usersID = new int[usersFromSearch.Count];
            for (int i = 0; i < usersID.Length; i++)
            {
                usersID[i] = usersFromSearch[i].Id;
            }
            return usersID;
        }
        #endregion

        #region Work with XML
        public void WriteToXML()
        {
            Logger.Trace("UserRepository.WriteToXML called");
            try
            {                
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
                string path = ConfigurationManager.AppSettings["xmlPath"];
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, Users);
                }
            }
            catch (InvalidOperationException ex)
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Error("Write to Xml " + ex.Message);
                }
            }
            catch (ConfigurationErrorsException exception)
            {
                if (BoolSwitch.Enabled)
                {
                    Logger.Error("Write to Xml " + exception.Message);
                }
            }
        }

        public void ReadFromXML()
        {
            Logger.Trace("UserRepository.ReadFromXML called");
            try
            {                
                string path = ConfigurationManager.AppSettings["xmlPath"];
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));                
                List<User> newUsers = new List<User>();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    newUsers = (List<User>)formatter.Deserialize(fs);
                    Users = newUsers;
                }                               
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error("Read to Xml " + ex.Message);
            }
        }
        #endregion
    }
}
