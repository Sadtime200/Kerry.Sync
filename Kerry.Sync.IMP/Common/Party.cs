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

        public void PartyCompare()
        {
            string k3sql = "select partyid from fmparty where status = 'C'";
            string k35sql = "select company_code  from tb_company where status = 'C'  ";

            var k3PartyIDs = new List<string>();
            var k35PartyIDs = new List<string>();

            _task.taskRunner(() => this.CacheIfNotExists(k3sql, ComposePath(_k3Partys), DB_K3, ref k3PartyIDs));
            _task.taskRunner(() => this.CacheIfNotExists(k35sql, ComposePath(_k35Partys), DB_K35, ref k35PartyIDs));

            var missPartyIDs = k3PartyIDs.Except(k35PartyIDs).ToList();
            if (missPartyIDs != null)
            {
                JsonHelper.ObjectToText(missPartyIDs, ComposePath(_missPartys));
            }


        }

        private string ComposePath(string fn)
        {
             var _appPath = System.Environment.GetEnvironmentVariable("AppData") as string;

            return _appPath + "\\" + fn;
        }

        private List<string> CacheIfNotExists(string sql, string path ,IDBFactory df ,ref List<string> list)
        {
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

            return list;
        }
    }
}
