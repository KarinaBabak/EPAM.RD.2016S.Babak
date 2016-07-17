using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UserStorage;
using UserStorage.Repository;
using UserStorage.Validator;

namespace IteratorUnitTest
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_UserNull_ReturnException()
        {
            UserValidator validator = new UserValidator();
            var result = validator.Validate((User)null);
        }


        [TestMethod]
        public void Validate_AgeIsNotValid_ReturnFalse()
        {
            UserValidator validator = new UserValidator();
            User user = new User("Bogdanovich", "Max", new DateTime(1800, 7, 20, 18, 30, 25),
                Gender.Male);

            var result = validator.Validate(user);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Validate_ValidData_ReturnTrue()
        {
            UserValidator validator = new UserValidator();
            User user = new User("Bogdanovich", "Max", new DateTime(1950, 7, 20, 18, 30, 25),
                Gender.Male);
            var result = validator.Validate(user);
            Assert.AreEqual(true, result);
        }
    }
}
