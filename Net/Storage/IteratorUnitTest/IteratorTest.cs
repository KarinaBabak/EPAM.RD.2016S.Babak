using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Iterator;
using UserStorage;
using UserStorage.Interfaces;

namespace IteratorUnitTest
{
    [TestClass]
    public class IteratorTest
    {
        #region IsPrime Tests
        [TestMethod]
        public void IsPrime_NumberLessZero_ReturnFalse()
        {
            bool result = (-3).IsPrime();

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsPrime_NumberLessTwo_ReturnFalse()
        {
            bool result = (1).IsPrime();

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsPrime_NumberIsPrime_ReturnTrue()
        {
            bool result = (11).IsPrime();

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }
        #endregion

        [TestMethod]
        public void GetNext_ReturnTwo()
        {
            CustomIterator iterator = new CustomIterator();
            var result = iterator.GetNext();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Reset_ReturnTwo()
        {
            CustomIterator iterator = new CustomIterator();
            User user = new User("Bogdanovich", "Max", new DateTime(1960, 7, 20, 18, 30, 25), Gender.Male);
            User user1 = new User("Mirnyi", "Max", new DateTime(1970, 7, 20, 18, 30, 25), Gender.Male);
            UserRepository rep = new UserRepository();
            rep.Add(user);
            rep.Add(user1);
            rep.Add(new User());
            iterator.Reset();
            int result = iterator.Current;
            Assert.AreEqual(1, result);
        }
    }
}
