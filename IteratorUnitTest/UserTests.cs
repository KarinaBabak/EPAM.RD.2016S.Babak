using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UserStorage;
using UserStorage.Repository;
using UserStorage.Validator;

namespace IteratorUnitTest
{
    [TestClass]
    public class UserTests
    {
        #region override Methods
        [TestMethod]
        public void Equals_CheckNullArgument_ReturnFalse()
        {
            User user1 = new User();
            User user2 = null;
            var result = user1.Equals(user2);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Equals_ArgumentTheSameObject_ReturnTrue()
        {
            User user = new User();            
            var result = user.Equals(user);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetHashCode_ReturnTrue()
        {
            User user = new User("Max", "Bogdanovich", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            var user1 = user;
            Assert.AreEqual(user.GetHashCode(), user1.GetHashCode());
        }
        #endregion

        #region Add
        [TestMethod]
        public void Add_NewUser_ReturnIdTwo()
        {
            User user = new User();
            UserRepository rep = new UserRepository();
            var result = rep.Add(user);  
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_UserIsExist_ReturnException()
        {
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            User user1 = new User("Mirnyi", "Max", new DateTime(1970, 7, 20, 18, 30, 25),
                Gender.Male);
            UserRepository rep = new UserRepository();
            rep.Add(user);
            rep.Add(user1);
            rep.Add(user);            
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Delete_ReturnTrue()
        {
            User user = new User();
            UserRepository rep = new UserRepository();
            int id = rep.Add(user);
            rep.Delete(user);
            bool resultIsUserExist = true; 
            if (rep.SearchForUser(u => u.Id == user.Id).FirstOrDefault().ToString() == id.ToString())
                resultIsUserExist = false;
            Assert.AreEqual(true, resultIsUserExist);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_UserIsNotExist_ReturnException()
        {
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            UserRepository rep = new UserRepository();           
            rep.Delete(user);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ReturnException()
        {            
            UserRepository rep = new UserRepository();
            rep.Delete(null);
        }
        #endregion

        #region Search
        [TestMethod]
        public void GetUserByPredicate_ReturnTrue()
        {
            UserRepository rep = new UserRepository();
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            User user1 = new User("Mirnyi", "Max", new DateTime(1970, 7, 20, 18, 30, 25),
                Gender.Male);
            rep.Add(user);
            rep.Add(user1);
            User userBySearch = rep.GetUserByPredicate(u => u.LastName == user.LastName);
            bool result = false;
            if (userBySearch.Equals(user)) result = true;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetById_ReturnTrue()
        {
            UserRepository rep = new UserRepository();
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            User user1 = new User("Mirnyi", "Max", new DateTime(1970, 7, 20, 18, 30, 25),
                Gender.Male);            
            rep.Add(user);
            int id = rep.Add(user1);
            User userBySearch = rep.GetById(id);
            bool result = false;
            if (userBySearch.Equals(user1)) result = true;
            Assert.AreEqual(true, result);            
        }

        [TestMethod]
        public void SearchForUser_SearchByFirstName_ReturnTrue()
        {
            UserRepository rep = new UserRepository();
            User user = new User("Max", "Bogdanovich", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            User user1 = new User("Max", "Mirnyi", new DateTime(1970, 7, 20, 18, 30, 25),
                Gender.Male);
            User user2 = new User("Vika", "Azarenko", new DateTime(1980, 7, 20, 18, 30, 25),
                Gender.Female);
            rep.Add(user);
            rep.Add(user1);
            rep.Add(user2);
            var res = rep.SearchForUser(u => u.FirstName == "Max");
            bool result = false;
            if (res.Count() == 2) result = true;
            Assert.AreEqual(true, result);
        }
        #endregion

        #region XML
        [TestMethod]
        public void XML_ReturnTrue()
        {
            UserRepository rep = new UserRepository();
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male);
            User user1 = new User("Mirnyi", "Max", new DateTime(1970, 7, 20, 18, 30, 25),
                Gender.Male);
            rep.Add(user);
            rep.Add(user1);
            var amount = rep.GetAll().Count();
            rep.WriteToXML();            
            rep.ReadFromXML();
            var amount1 = rep.GetAll().Count();
            bool result = (amount == amount1);
            Assert.AreEqual(true, result);            
        }
        #endregion
    }
}
