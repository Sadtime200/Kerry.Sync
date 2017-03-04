using Kerry.Sync.IMP.Constants;
using Kerry.Sync.IMP.Model;
using Kerry.Sync.Utility.DB;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.Utility.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP
{
    public  class CompanyAccountFactory
    {
        #region Initial Region
        private K3DBFactory _k3DB = new K3DBFactory();
        private K35DESTDB _k35DESTDB = new K35DESTDB();
        private TaskHelper _task = new TaskHelper();
        #endregion

        public bool SynK3ToK35CompanyAcc()
        {
            var missPartyIDs = JsonHelper.TextToJson<List<string>>(string.Concat(CommonConstants.APPDATA_PATH, "\\", CommonConstants.MISS_PARTYS_FILE_PATH));
            var companys = new List<Company>();
            var companyAccounts = new List<CompanyAccountFactory>();

            var sql = string.Empty;

            StringBuilder sb = new StringBuilder();
            StringBuilder insertStr = new StringBuilder();

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

                sql = GetK3PartyAcc(sb);
                try
                {
                    var rows = _k3DB.ExecuteDataTable(sql).Rows;
                    if (rows != null && rows.Count != 0)
                    {
                        insertStr = insertStr.Append(InitialInsertStr());
                        foreach (DataRow r in rows)
                        {
                            insertStr = InsertToK35(insertStr, r);
                        }
                        var insertSql = insertStr.ToString().Remove(insertStr.Length - 1);

                        //Clear String
                        insertStr = insertStr.Clear();
                        sb.Clear();

                        //Batch Insert
                        _k35DESTDB.ExecuteNonQuery(insertSql);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;
        }


        #region  Sql Part
        private static string GetK3PartyAcc(StringBuilder sb)
        {
            return string.Format(@"
                        SELECT P.PARTYID COMPANYCODE, A.SNO,A.BIZTYPE,A.CURRCODE,A.ACCOUNTTYPE,ACNO MAPCODE, OWNERID STATIONCODE FROM FMPARTY P 
                            INNER JOIN FMPARTYACC A 
                            ON P.PARTYID = A.PARTYID
                             WHERE P.STATUS = 'C' 
                        and P.PARTYID IN ({0})
                    ", sb.ToString());
        }

        private static string InitialInsertStr()
        {
            var initialInser = @"insert ignore into tb_company_account 
                                (company_id,sno,biztype,currency,account_type,mapcode,station_code,create_by,
                                update_by,CREATE_TIMESTAMP,UPDATE_TIMESTAMP) values ";
           
            return initialInser;
        }

        private static StringBuilder InsertToK35(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}),",
            "(select id from tb_company where company_code = '" + r["COMPANYCODE"] + "' limit 1)", "'" + r["SNO"] + "'", "'" + r["BIZTYPE"] + "'", "'" + r["CURRCODE"] + "'", "'" + r["ACCOUNTTYPE"] + "'",
            "'" + r["MAPCODE"] + "'", "'" + r["STATIONCODE"] + "'", "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion

    }




}
