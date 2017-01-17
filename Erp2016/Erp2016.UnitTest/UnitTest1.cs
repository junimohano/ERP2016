using System;
using Erp2016.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Erp2016.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mail = new CMail();
            //mail.SendMail(CConstValue.Approval.BusinessTrip, CConstValue.MailStatus.Requested, 1, 1762, 577, "TEST001");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mail = new CMail();
            //mail.SendMail(CConstValue.Approval.BusinessTrip, CConstValue.MailStatus.IsBeingApproved, 1, 1762, 577, "TEST001");
        }


        [TestMethod]
        public void TestMethod3()
        {
            var mail = new CMail();
            //mail.SendMail(CConstValue.Approval.BusinessTrip, CConstValue.MailStatus.Approved, 1, 1762, 577, "TEST001");
        }
    }
}
