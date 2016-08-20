using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorage;
using UserStorage.Interfaces;
using UserStorage.Validator;
using UserStorage.SearchCriteria;

namespace IteratorUnitTest
{
    [TestClass]
    public class UserTests
    {
        private User user1 = new User("Max", "Bogdanovich", new DateTime(1960, 7, 20, 18, 30, 25), Gender.Male);

        private User user2 = new User("Max", "Mirnyi", new DateTime(1970, 7, 20, 18, 30, 25), Gender.Male);

        private UserRepository repository = new UserRepository();
        
        #region override Methods
        [TestMethod]
        public void Equals_CheckNullArgument_ReturnFalse()
        {            
            User user = null;
            var result = user1.Equals(user);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Equals_ArgumentTheSameObject_ReturnTrue()
        {
            var result = user1.Equals(user1);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetHashCode_ReturnTrue()
        {
            User user = new User("Max", "Bogdanovich", new DateTime(1960, 7, 20, 18, 30, 25), Gender.Male);
            var user1 = user;
            Assert.AreEqual(user.GetHashCode(), user1.GetHashCode());
        }
        #endregion

        #region Add
        [TestMethod]
        public void Add_NewUser_ReturnIdTwo()
        {
            repository.Clear();
            User user = new User();
            var result = repository.Add(user);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_UserIsExist_ReturnException()
        {
            repository.Clear();
            repository.Add(user1);
            repository.Add(user2);
            repository.Add(user1);
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Delete_ReturnTrue()
        {
            User user = new User();
            int id = repository.Add(user);
            repository.Delete(user);
            bool resultIsUserExist = true;
            if (repository.SearchForUser(u => u.Id == user.Id).FirstOrDefault().ToString() == id.ToString())
            {
                resultIsUserExist = false;
            }
            Assert.AreEqual(true, resultIsUserExist);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_UserIsNotExist_ReturnException()
        {
            repository.Clear();
            repository.Delete(user1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ReturnException()
        {
            repository.Delete(null);
        }
        #endregion

        #region Search        

        [TestMethod]
        public void GetById_ReturnTrue()
        {
            repository.Clear();
            repository.Add(user1);
            int id = repository.Add(user2);
            User userBySearch = repository.GetById(id);
            bool result = false;

            if (userBySearch.Equals(user2))
            {
                result = true;
            }

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetById_NotExistId_ReturnFalse()
        {
            repository.Clear();
            repository.Add(user1);            
            User userBySearch = repository.GetById(11);
            bool result = false;

            if (userBySearch != null)
            {
                result = true;
            }

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void SearchForUser_SearchByFirstName_ReturnTrue()
        {
            repository.Clear();
            User user3 = new User("Vika", "Azarenko", new DateTime(1980, 7, 20, 18, 30, 25), Gender.Female);
            repository.Add(user1);
            repository.Add(user2);
            repository.Add(user3);
            var res = repository.SearchForUser(u => u.FirstName == "Max");
            bool result = false;
            if (res.Count() == 2)
            {
                result = true;
            }

            Assert.AreEqual(true, result);
        }
       
        #endregion

        #region XML
        [TestMethod]
        public void XML_ReturnTrue()
        {
            repository.Clear();
            repository.Add(user1);
            repository.Add(user2);
            var amount = repository.GetAll().Count();
            repository.WriteToXML();
            repository.ReadFromXML();
            var amount1 = repository.GetAll().Count();
            bool result = (amount == amount1);
            Assert.AreEqual(true, result);
        }
        #endregion
    }
}
