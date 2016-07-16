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
        public void Add_NewUser_ReturnIdTwo()
        {
            User user = new User();
            UserRepository rep = new UserRepository();
            var result = rep.Add(user);  
            Assert.AreEqual(2, result);
        }

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

       
    }
}
