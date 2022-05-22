using BitWiseOperator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class TestDemoBusinessObject
    {
        /// <summary>
        /// Test xem viec trao quyen, va kiem tra quyen co thuc hien dung
        /// </summary>
        [TestMethod]
        public void TestYouHaveRight()
        {
            //arrange
            DemoBusinessObject BO = new DemoBusinessObject();
            List<ACL> myACL = new List<ACL>() { ACL.View, ACL.Edit, ACL.MarkDel };
            bool expected = true;
            BO.SetRight(myACL);//User co cac quyen mo ta trong ACL tuong ung voi Object name =Demo
            //act
            ACL acl = ACL.Edit; //test factor
            bool actual = BO.HasRight(acl);
            //Assert
            //Assert.IsTrue(BO.HasRight(acl), "you have right {0}", acl.ToString()) ; //vi trong ds co quyen do thi no phai tra ve true
            Assert.AreEqual(expected, actual, "you have right {0}", acl.ToString());
        }
        [TestMethod]
        public void TestYouNotHaveRight()
        {
            //arrange
            DemoBusinessObject BO = new DemoBusinessObject();
            List<ACL> myACL = new List<ACL>() { ACL.View, ACL.Edit, ACL.MarkDel };
            bool expected = false;
            BO.SetRight(myACL);//User co cac quyen mo ta trong ACL tuong ung voi Object name =Demo
            //act
            ACL acl = ACL.Create; //test factor
            bool actual = BO.HasRight(acl);
            //Assert
            //Assert.IsTrue(BO.HasRight(acl), "you have right {0}", acl.ToString()) ; //vi trong ds co quyen do thi no phai tra ve true
            Assert.AreEqual(expected, actual, "you have no right {0}", acl.ToString());
        }
        /// <summary>
        /// Test cac case throw exception
        /// </summary>
        //[TestMethod]
        //public void Test_throw()
        //{

        //}
    }
}
