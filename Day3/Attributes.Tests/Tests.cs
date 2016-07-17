using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Attributes;
using System.Reflection;
using System.ComponentModel;

namespace Attributes.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void StringValidator_PropertiesUser_ReturnTrue()
        {            
            Type userType = typeof(User);
            //InstantiateUserAttribute attr = new InstantiateUserAttribute("Karina", "Babak");
            //InstantiateUserAttribute[] instantiateUserAttributes =
            //    (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userType, typeof(InstantiateUserAttribute));
            //MatchParameterWithPropertyAttribute[] matchParameter =
            //    (MatchParameterWithPropertyAttribute[])Attribute.GetCustomAttributes(userType.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));

            //AttributeCollection attributes = TypeDescriptor.GetProperties(this)["Id"].Attributes;
            //DefaultValueAttribute myAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
            //User user = new User((int)myAttribute.Value);

            StringValidatorAttribute[] firstNameValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("FirstName"), typeof(StringValidatorAttribute));   
            StringValidatorAttribute[] lastNameValid = (StringValidatorAttribute[])Attribute.GetCustomAttributes(userType.GetProperty("FirstName"), typeof(StringValidatorAttribute));   
            //bool result = false;
            //if ((user.FirstName.Length > firstNameValid[0].Length) &&
            //    (user.LastName.Length > lastNameValid[0].Length))
            //    result = true;
            //Assert.AreEqual(true, result);
        }
    }
    
}

