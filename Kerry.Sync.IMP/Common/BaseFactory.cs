using Kerry.Sync.IMP.Constants;
using Kerry.Sync.IMP.Model;
using Kerry.Sync.Utility.DB;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.Utility.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kerry.Sync.IMP.Common
{
    public abstract class BaseFactory : IBaseFactory
    {

        #region Initial Region
        protected K3DBFactory _k3DB = new K3DBFactory();
        protected K35DESTDB _k35DESTDB = new K35DESTDB();
        protected TaskHelper _task = new TaskHelper();
        #endregion

        #region Implementation
        public virtual bool SynK3Data(out int k3Rows ,out int k35Rows)
        {

            var missPartyIDs = JsonHelper.TextToJson<List<string>>(string.Concat(CommonConstants.APPDATA_PATH, "\\", CommonConstants.MISS_PARTYS_FILE_PATH));
            var sql = string.Empty;
            k35Rows = 0;
            k3Rows = 0;
            StringBuilder sb = new StringBuilder();
            StringBuilder insertStr = new StringBuilder();

            //Batch Insert Size .
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

                sql = GetK3Data(sb);
                try
                {
                    var rows = _k3DB.ExecuteDataTable(sql).Rows;
                    if (rows != null && rows.Count != 0)
                    {
                        k3Rows += rows.Count;
                        insertStr = insertStr.Append(InitialInsertStr());
                        foreach (DataRow r in rows)
                        {
                            insertStr = InsertK3Data(insertStr, r);
                        }
                        var insertSql = insertStr.ToString().Remove(insertStr.Length - 1);

                        k35Rows += _k35DESTDB.ExecuteNonQuery(insertSql);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                //Clear String
                insertStr = insertStr.Clear();
                sb.Clear();
            }
            var currentClass = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            var currentMethod = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ConsoleWrite(k3Rows, k35Rows, currentClass, currentMethod);
            return true;
        }

        private static void ConsoleWrite(int k3Rows, int k35Rows, string currentClass, string currentMethod)
        {
            Console.WriteLine("Executing Class: " + currentClass + " Method: " + currentMethod + ". Impact K3 rows: " + k3Rows + " Impact K35 rows: " + k35Rows);
        }

        #endregion

        #region Sql Logic
        public abstract string GetK3Data(StringBuilder sb);
        public abstract string InitialInsertStr();
        public abstract StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r);
        #endregion



    }
}
