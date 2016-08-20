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
    public class UserRepository : MarshalByRefObject, IUserRepository
    {
        /// <summary>
        /// NLog field
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// BooleanSwitch field for activating logging
        /// </summary>
        private static readonly BooleanSwitch BoolSwitch = new BooleanSwitch("Switch", string.Empty);
                    
        /// <summary>
        /// Generator id
        /// </summary>
        private ICustomIterator iterator;
        
        /// <summary>
        /// User validation
        /// </summary>
        private UserValidator validator;        

        #region ctor

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="generator">Generator id</param>
        /// <param name="validator">Validator for user</param>
        public UserRepository(ICustomIterator generator = null, UserValidator validator = null)
        {            
            Users = new List<User>();
            this.iterator = generator ?? new CustomIterator();
            this.validator = validator ?? new UserValidator();            
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserRepository()
        {
            Users = new List<User>();
            iterator = new CustomIterator();
            validator = new UserValidator();            
        }
        #endregion

        public List<User> Users { get; private set; }

        /// <summary>
        /// Delete user from repository
        /// </summary>
        /// <param name="user">user for removing</param>
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
            {
                throw new ArgumentException("The user is not exist");
            }
        }

        /// <summary>
        /// Determination of adding a new user in list with all users
        /// </summary>
        /// <param name="user">the new user</param>
        /// <returns>id of the new user</returns>
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

        /// <summary>
        /// Determination of getting all users from repository
        /// </summary>
        /// <returns>collection with all users</returns>
        public IEnumerable<User> GetAll()
        {
            Logger.Trace("UserRepository.GetAll called");           
            return Users;
        }

        /// <summary>
        /// Determination of removing all users from collection
        /// </summary>
        public void Clear()
        {
            Logger.Trace("UserRepository.Clear called");
            Users.Clear();
        }

        #region Search User

        /// <summary>
        /// Getting user by id
        /// </summary>
        /// <param name="id">id of user for search</param>
        /// <returns>the user with searched id</returns>
        public User GetById(int id)
        {
            return Users.Where(u => u.Id == id).FirstOrDefault();
        }             

        /// <summary>
        ///  Search users by criteria
        /// </summary>
        /// <param name="criteria">criteria for searching</param>
        /// <returns>users id</returns>
        public List<int> SearchForUser(Func<User, bool> criteria)
        {
            var usersId = Users.Where(criteria).Select(u => u.Id).ToList();
            if (ReferenceEquals(usersId, null))
            {
                return null;
            }

            return usersId;
        }

        /// <summary>
        /// Search user by criteria
        /// </summary>
        /// <param name="criteria">criteria for search</param>
        /// <returns>collection of users id</returns>
        public IEnumerable<int> SearchForUsers(ISearchСriterion<User>[] criteria)
        {
            List<int> idUsersFromSearch = Users.Where(u => criteria.All(c => c.MatchByCriterion(u))).Select(i => i.Id).ToList();

            return idUsersFromSearch;
        }
        #endregion

        #region Work with XML
        /// <summary>
        /// Writing all users to xml file
        /// </summary>
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

        /// <summary>
        /// Reading users from xml file
        /// </summary>
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
                    iterator.Current = Users.LastOrDefault().Id;
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
