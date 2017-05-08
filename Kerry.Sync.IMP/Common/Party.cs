using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kerry.Sync.Utility.DB;
using System.Data;
using Kerry.Sync.Utility.Text;
using Kerry.Sync.Utility;
using Kerry.Sync.Utility.TaskManger;

namespace Kerry.Sync.IMP.Common
{
  public  class Party
    {

        private K3DBFactory DB_K3 = new K3DBFactory();
        private K35DBFactory DB_K35 = new K35DBFactory();
        private TaskHelper _task = new TaskHelper();

        private string _k3Partys = "K3PARTYS.txt";
        private string _k35Partys = "K35PARTYS.txt";
        private string _missPartys = "MissPartys.txt";

        /// <summary>
        /// Compare K3 parties with K3 parties 
        /// </summary>
        /// <param name="k3Rows">Total K3 Parties</param>
        /// <param name="k35Rows">Total K35 Parties</param>
        /// <param name="missRows">Missing Parties Number</param>
        public void PartyCompare(out int k3Rows,out int k35Rows, out int missRows)
        {
            k3Rows = 0;
            k35Rows = 0;
            missRows = 0;
            string k3sql = "select partyid from fmparty where status = 'C'";
            string k35sql = "select company_code  from tb_company where status = 'C'  ";

            var k3PartyIDs = new List<string>();
            var k35PartyIDs = new List<string>();

            var k3ImpartRows = 0;
            var k35ImpartRows = 0;

            _task.taskRunner(() => this.CacheIfNotExists(k3sql, ComposePath(_k3Partys), DB_K3, ref k3PartyIDs, ref k3ImpartRows));
            _task.taskRunner(() => this.CacheIfNotExists(k35sql, ComposePath(_k35Partys), DB_K35, ref k35PartyIDs , ref k35ImpartRows));


            k3Rows = k3ImpartRows;
            k35Rows = k35ImpartRows;
            //var missPartyIDs = k3PartyIDs.Except(k35PartyIDs).ToList();
            var missPartyIDs = k35PartyIDs.Except(k3PartyIDs).ToList();
            if (missPartyIDs != null)
            {
                JsonHelper.ObjectToText(missPartyIDs, ComposePath(_missPartys));
            }
            var currentClass = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            var currentMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ConsoleWrite(k3Rows, k35Rows, currentClass, currentMethod);

        }



        private string ComposePath(string fn)
        {
             var _appPath = System.Environment.GetEnvironmentVariable("AppData") as string;

            return _appPath + "\\" + fn;
        }

        private List<string> CacheIfNotExists(string sql, string path ,IDBFactory df ,ref List<string> list , ref int rows)
        {
            rows = 0;
            //var list = new List<string>();
            if (!TextHelper.TryGetValue(path, out list))
            {
                list = new List<string>();
                var dt = df.ExecuteDataTable(sql).Rows;
                for (int i = 0; i < dt.Count; i++)
                {
                    list.Add(dt[i][0] as string);
                }
                JsonHelper.ObjectToText<List<string>>(list, path);
            }
            rows = list.Count;
            return list;
        }
        private static void ConsoleWrite(int k3Rows, int k35Rows, string currentClass, string currentMethod)
        {
            Console.WriteLine("Executing Class: " + currentClass + " Method: " + currentMethod + ". Impact K3 rows: " + k3Rows + " Impact K35 rows: " + k35Rows);
        }
    }
}
