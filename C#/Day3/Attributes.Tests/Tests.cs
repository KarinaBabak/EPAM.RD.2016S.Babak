using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Attributes;
using System.Reflection;
using System.ComponentModel;


namespace Attributes.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void User_ReturnTrue()
        {
            Type userType = typeof(User);
            InstantiateUserAttribute[] instantiateUserAttributes =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));
            User user = new User(instantiateUserAttributes[2].Id);
            user.FirstName = instantiateUserAttributes[2].ProperyFirstName;
            user.LastName = instantiateUserAttributes[2].PropertyLastName;
            bool result = false;
            if (user.FirstName == "Semen" && user.Id == 2)
                result = true;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void User_DefaultID_ReturnTrue()
        {
            Type userType = typeof(User);
            InstantiateUserAttribute[] instantiateUserAttributes =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));
            User user = new User(instantiateUserAttributes[0].Id);
            user.FirstName = instantiateUserAttributes[0].ProperyFirstName;
            user.LastName = instantiateUserAttributes[0].PropertyLastName;
            bool result = false;
            if (user.FirstName == "Alexander" && user.Id == 1)
                result = true;

            Assert.AreEqual(true, result);
        }

       
        #region StringValidator

        [TestMethod]
        public void StringValidatorAttribute_NotValidFirstName_ReturnFalse()
        {
            Type userType = typeof(User);
            User user = new User(5);
            user.FirstName = new string('a', 32);
            user.LastName = "Numbers";
            
            StringValidatorAttribute[] strFirstValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("FirstName"), typeof(StringValidatorAttribute));
            bool valid = true;
            if (user.FirstName.Length > strFirstValid[0].Length)
                valid = false;

            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void StringValidatorAttribute_NotValidLastName_ReturnFalse()
        {
            Type userType = typeof(User);
            User user = new User(5);
            user.FirstName = new string('a', 29);
            user.LastName = new string('a', 32);
                        
            StringValidatorAttribute[] lastValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("LastName"), typeof(StringValidatorAttribute));
            bool valid = true;
            if (user.LastName.Length > lastValid[0].Length)
                valid = false;

            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void StringValidatorAttribute_NotValid_ReturnFalse()
        {
            Type userType = typeof(User);
            User user = new User(5);
            user.FirstName = new string('a', 30);
            user.LastName = new string('a', 30);

            StringValidatorAttribute[] lastValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("LastName"), typeof(StringValidatorAttribute));
            bool valid = true;
            if (user.LastName.Length > lastValid[0].Length)
                valid = false;

            Assert.AreEqual(false, valid);
        }
        #endregion

        [TestMethod]
        public void IntValidatorAttribute_NotValidMaxId_ReturnFalse()
        {
            Type userType = typeof(User);
            User user = new User(10000000);
            user.FirstName = "Vika";
            user.LastName = "Azarenko";

            IntValidatorAttribute att = new IntValidatorAttribute();
            int max = att.Max;            
            bool valid = true;
            if (user.Id > max)
                valid = false;

            Assert.AreEqual(false, valid);
        }
    }
    
}

