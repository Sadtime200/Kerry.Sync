using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kerry.Sync.Utility;
using System.Data.Common;
using System.Data;
using Kerry.Sync.Utility.DB;

namespace Kerry.Sync.Data.Test
{
    [TestClass]
    public class UnitTest1
    {
        //private K35DBFactory _k35DB = new K35DBFactory() ;

        [TestMethod]
        public void TestMethod1()
        {
            //K35DBFactory _


            //_k35DB.c

            string sql = "select * from tb_company limit 5;";
            var _k35DB = new K35DBFactory();
            var dt = _k35DB.ExecuteDataTable(sql);
           //var dt =  GetDataViaSql(sql);

        }

     
    }
}
