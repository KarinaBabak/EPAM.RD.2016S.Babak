using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Iterator;

namespace IteratorUnitTest
{
    [TestClass]
    public class IteratorTest
    {
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

        [TestMethod]
        public void GetNext_ReturnTwo()
        {
            CustomIterator iterator = new CustomIterator();
            var result = iterator.GetNext();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }
    }
}
