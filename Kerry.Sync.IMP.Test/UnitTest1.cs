using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kerry.Sync.IMP.Common;
using Kerry.Sync.Utility.Text;
using System.Reflection;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.IMP.Factory;

namespace Kerry.Sync.IMP.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string  k = "3's";

             k = k.ToString();
            k = TextHelper.Escape(k);
            var p = new Party();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var e = new CompanyFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows,out k35Rows);


        }

        [TestMethod]
        public void TestMethod3()
        {
            var e = new CompanyAccountFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows, out k35Rows);

        }

        [TestMethod]
        public void TestMethod4()
        {
            var e = new CompanyConstantFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows, out k35Rows);
        }
        [TestMethod]
        public void TestMethod5()
        {
            var e = new CompanyRelFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows, out k35Rows);
        }
        [TestMethod]
        public void TestMethod6()
        {
            var e = new CompanyRoleRelFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows, out k35Rows);
        }
        [TestMethod]
        public void TestMethod7()
        {
            var e = new CompanyVariantFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows, out k35Rows);
        }




        [TestMethod]
        public void TestMethod8()
        {
            var e = new CompanyOthersStatusFactory();
            int rows = 0;
           
        }
        [TestMethod]
        public void TestMethod9()
        {
            #region Initial Region

            int k3Rows;
            int k35Rows;
            int missParties;
            var p = new Party();
            var company = new CompanyFactory();
            var companyAccount = new CompanyAccountFactory();
            var companyConstant = new CompanyConstantFactory();
            var companyCredit = new CompanyFactory();
            var companyRel = new CompanyRelFactory();
            var companyRole = new CompanyRoleRelFactory();
            var companyVariant = new CompanyVariantFactory();
            var companyOtherStatus = new CompanyOthersStatusFactory();
            var task = new TaskHelper();
            #endregion


            task.taskRunner(()=>p.PartyCompare(out k3Rows, out k35Rows, out missParties));
            task.taskRunner(()=> company.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyAccount.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyConstant.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyCredit.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyRel.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyRole.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyVariant.SynK3Data(out k3Rows, out k35Rows));
            task.taskRunner(()=> companyOtherStatus.SyncK35CompanyStatus(out k35Rows));
            




        }

        [TestMethod]
        public void TestMethod10()
        {
            var e = new AgentContractFactory();
            int k3Rows;
            int k35Rows;
            e.SynK3Data(out k3Rows, out k35Rows);

        }
    }
}
