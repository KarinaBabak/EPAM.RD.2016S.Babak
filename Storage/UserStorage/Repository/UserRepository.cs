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
using System.Diagnostics;

using UserService.Interfaces;
using UserService;

namespace UserStorage.Repository
{
    public class UserRepository : IUserRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly BooleanSwitch boolSwitch = new BooleanSwitch("Switch", string.Empty);
        
        private List<User> Users { get; set; }
        public IService Service{get; private set;}
        private ICustomIterator iterator;        
        private UserValidator validator;
        

        public UserRepository(ICustomIterator generator = null, UserValidator validator = null)
        {
            Service = new MasterService(this);
            Users = new List<User>();
            this.iterator =  generator ?? new CustomIterator();
            this.validator = validator ?? new UserValidator();            
        }

        public UserRepository()
        {
            Users = new List<User>();
            iterator = new CustomIterator();
            validator = new UserValidator();
            Service = new MasterService(this);
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
            {                
                Users.Remove(userForRemoving);
                Service.Delete(user);
            }
            else
                throw new ArgumentException("The user is not exist");
        }

        public int Add(User user)
        {
            logger.Trace("UserRepository.Add called. Create the user: "+ user.ToString());            
           
            if (!validator.Validate(user))
            {
                if (boolSwitch.Enabled)
                {
                    logger.Error("The validation of new user is failed");
                }
                throw new ArgumentException("The validation is failed");
            }
            if (Users.Contains(user))
            {
                if (boolSwitch.Enabled)
                {
                    logger.Error("The user already exists");
                }
                throw new InvalidOperationException("User already exists");
            }

            user.Id = iterator.GetNext();            
            Users.Add(user);
            Service.Add(user);
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
                XMLWorker xmlWorker = new XMLWorker();
                string path = ConfigurationManager.AppSettings["xmlPath"];
                xmlWorker.WriteToXML(Users, path);
            }
            catch(InvalidOperationException ex)
            {
                if (boolSwitch.Enabled)
                {
                    logger.Error("Write to Xml " + ex.Message);
                }
            }
            catch(ConfigurationErrorsException exception)
            {
                if (boolSwitch.Enabled)
                {
                    logger.Error("Write to Xml " + exception.Message);
                }
            }
        }

        public void ReadFromXML()
        {
            logger.Trace("UserRepository.ReadFromXML called");
            try
            {                
                string path = ConfigurationManager.AppSettings["xmlPath"];
                XMLWorker xmlWorker = new XMLWorker();
                Users = xmlWorker.ReadFromXML(path);                               
            }
            catch (InvalidOperationException ex)
            {
                logger.Error("Read to Xml " + ex.Message);
            }
        }
        #endregion

    }
}
