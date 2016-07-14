using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using UserStorage;

namespace IteratorUnitTest.XmlTests
{
    [TestClass]
    public class XmlWorkerTests
    {
        [TestMethod]
        public void WriteToXML_Return()
        {
            List<User> users = new List<User>();
            users.Add(new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25),
                Gender.Male));
            users.WriteToXML();
            
        }
    }
}
