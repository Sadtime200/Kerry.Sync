using Kerry.Sync.IMP.Constants;
using Kerry.Sync.Utility.DB;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.Utility.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Kerry.Sync.IMP
{
    public class CompanyOthersStatusFactory
    {
        #region Initial Region
        protected K3DBFactory _k3DB = new K3DBFactory();
        protected K35DESTDB _k35DESTDB = new K35DESTDB();
        protected TaskHelper _task = new TaskHelper();
        #endregion
        public bool SyncK35CompanyStatus(out int impactRows)
        {
             impactRows = 0;
            var missPartyIDs = JsonHelper.TextToJson<List<string>>(string.Concat(CommonConstants.APPDATA_PATH, "\\", CommonConstants.MISS_PARTYS_FILE_PATH));
            var sql = string.Empty;
            StringBuilder sb = new StringBuilder();
            StringBuilder insertStr = new StringBuilder();

            //Batch Update Size .
            int size = 100;

            for (int i = 0; i <= missPartyIDs.Count + size; i += size)
            {
                sb.Append("''");
                for (int j = 0; j < size; j++)
                {
                    try
                    {

                        sb = sb.Append(string.Concat(",'", missPartyIDs[i + j], "'"));
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }

                sql = UpdateK35(sb);
                try
                {
                     impactRows += _k35DESTDB.ExecuteNonQuery(sql);
                }
                catch (Exception ex)
                {

                    return false;
                }
               
                //Clear String
                //insertStr = insertStr.Clear();
                sb.Clear();
            }

            var currentClass = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            var currentMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ConsoleWrite(impactRows, currentClass, currentMethod);

            return true;



        }
        private static void ConsoleWrite(int impactRows, string currentClass, string currentMethod)
        {
            Console.WriteLine("Executing Class: " + currentClass + " Method: " + currentMethod + ". Impact  rows: " + impactRows );
        }
        public  string UpdateK35(StringBuilder sb)
        {
            return string.Format(@"
                       UPDATE TB_COMPANY SET STATUS = 'C' WHERE COMPANY_CODE IN ({0})  AND STATUS !='C'
                    ", sb.ToString());
        }

    }
}
