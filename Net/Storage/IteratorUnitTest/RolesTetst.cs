using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

using UserStorage;
using UserStorage.Interfaces;

namespace IteratorUnitTest
{
    [TestClass]
    public class RolesTetst
    {
        //[TestMethod]
        //public void NotifyObservers_ReturnTrue()
        //{
        //    UserRepository rep = new UserRepository();
        //    MasterService master = new MasterService(rep);
        //    SlaveService slave1 = new SlaveService(rep);
        //    master.RegisterObserver(slave1);
        //    User user = new User();
        //    master.Add(user);
        //    var result = slave1.SearchForUser(u => u.LastName == user.LastName);
        //    bool res = false;
        //    if (result.FirstOrDefault().ToString() == user.Id.ToString())
        //        res = true;
        //    Assert.AreEqual(true, res);
        //}
               
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void MasterCtor_CreateTwoMasters_Exception()
        //{
        //    UserRepository rep = new UserRepository();
        //    MasterService master = new MasterService(rep);
        //    master.Add(new User());
        //    MasterService master2 = new MasterService(rep);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void SlaveCtor_CreateFourSlaves_Exception()
        //{
        //    UserRepository rep = new UserRepository();
        //    MasterService master = new MasterService(rep);
        //    master.Add(new User());
        //    SlaveService slave1 = new SlaveService(rep);
        //    SlaveService slave2 = new SlaveService(rep);
        //    SlaveService slave3 = new SlaveService(rep);
        //    SlaveService slave4 = new SlaveService(rep);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void SlaveAdd_ReturnException()
        //{
        //    UserRepository rep = new UserRepository();
        //    MasterService master = new MasterService(rep);
        //    SlaveService slave1 = new SlaveService(rep);
        //    slave1.Add(new User());
        //}
    }
}
