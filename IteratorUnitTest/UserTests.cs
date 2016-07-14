using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UserStorage;
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
            var result = user.Id;
            Assert.AreEqual(2, result);
        }

        #region Validation Test
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_UserNull_ReturnException()
        {
            UserValidator validator = new UserValidator();            
            var result = validator.Validate((User)null);            
        }

        [TestMethod]        
        public void Validate_FirstNameIsNull_ReturnFalse()
        {
            UserValidator validator = new UserValidator();
            User user = new User();            
            user.DateOfBirth = new DateTime(1960, 7, 20, 18, 30, 25); 
            var result = validator.Validate(user);
        }
        #endregion
    }
}
