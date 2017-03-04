using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kerry.Sync.IMP.Common;
using Kerry.Sync.Utility.Text;

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
            p.PartyCompare();

        }

        [TestMethod]
        public void TestMethod2()
        {
            var e = new CompanyFactory();
            e.SynK3ToK35Company();
            

        }

        [TestMethod]
        public void TestMethod3()
        {
            var e = new CompanyAccountFactory();
            e.SynK3ToK35CompanyAcc();


        }
    }
}
