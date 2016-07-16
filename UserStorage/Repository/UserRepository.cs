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
using NLog;



namespace UserStorage.Repository
{
    public class UserRepository : IUserRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        
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
            else
                throw new ArgumentException("The user is not exist");
        }

        public int Add(User user)
        {
            logger.Trace("UserRepository.Add called");
            if (!validator.Validate(user))
            {
                logger.Error("The validation of new user is failed");
                throw new ArgumentException("The validation is failed");
            }
            if (Users.Contains(user))
            {
                logger.Error("The user already exists");
                throw new InvalidOperationException("User already exists");
            }

            user.Id = iterator.GetNext();
            Users.Add(user);
            return user.Id;
        }

        public IEnumerable<User> GetAll()
        {
            logger.Trace("UserRepository.GetAll called");
            return Users;
        }

        public void Clear()
        {
            logger.Trace("UserRepository.Clear called");
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
                return null;
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
            logger.Trace("UserRepository.WriteToXML called");
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
                string path = ConfigurationManager.AppSettings["xmlPath"];
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, Users);
                }
            }
            catch(InvalidOperationException ex)
            {
                logger.Error("Write to Xml " + ex.Message);
            }
            catch(ConfigurationErrorsException exception)
            {
                logger.Error("Write to Xml " + exception.Message);
            }
        }

        public void ReadFromXML()
        {
            logger.Trace("UserRepository.ReadFromXML called");
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
                string path = ConfigurationManager.AppSettings["xmlPath"];

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    List<User> newUsers = (List<User>)formatter.Deserialize(fs);
                    Users = newUsers;
                }
            }
            catch (InvalidOperationException ex)
            {
                logger.Error("Read to Xml " + ex.Message);
            }
        }
        #endregion

    }
}
