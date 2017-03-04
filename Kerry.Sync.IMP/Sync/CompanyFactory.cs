using Kerry.Sync.IMP.Constants;
using Kerry.Sync.IMP.Model;
using Kerry.Sync.Utility.DB;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.Utility.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kerry.Sync.IMP
{
    public class CompanyFactory
    {
        #region Initial Part
        private K3DBFactory _k3DB = new K3DBFactory();
        private K35DESTDB _k35DESTDB = new K35DESTDB();
        private TaskHelper _task = new TaskHelper();
        #endregion
        public bool SynK3ToK35Company()
        {
            var missPartyIDs = JsonHelper.TextToJson<List<string>>(string.Concat(CommonConstants.APPDATA_PATH, "\\", CommonConstants.MISS_PARTYS_FILE_PATH));

            var companys = new List<Company>();
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

                sql = GetK3Party(sb);
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
        private static string GetK3Party(StringBuilder sb)
        {
            return string.Format(@"
                        SELECT P.CITY, 
                          P.COUNTRY, 
                          P.PARTYID, 
                          P.FULLNAME, 
                          P.LOCALNAME, 
                          P.LSHORTNAME, 
                          P.DUPCHKKEY, 
                          S.SALESCODE, 
                          P.STATUS, 
                          P.REMARK, 
                          P.IATACODE, 
                          P.IATAACNO, 
                          A.PREFIX, 
                          A.CARRIERCODE, 
                          'K3' CREATEBY, 
                          'K3' UPDATEBY, 
                          SYSDATE CREATETIMESTAMP, 
                          SYSDATE UPDATETIMESTAMP 
                        FROM FMPARTY P 
                        LEFT JOIN FMAIRLINE A 
                        ON P.PARTYID =A.CARRIERID 
                        LEFT JOIN FMPARTYSALES S 
                        ON P.PARTYID   =S.PARTYID 
                        AND S.SNO      =1 
                        WHERE P.STATUS = 'C' 
                        and P.PARTYID IN ({0})
                    ", sb.ToString());
        }

        private static string InitialInsertStr()
        {
            var initialInser = @"insert ignore into tb_company ( 
                                    location_code,
                                    country_code,
                                    company_code,
                                    company_name_eng,
                                    company_name_local,
                                    short_name,
                                    company_dupkey,
                                    sales_user_code,
                                    status,
                                    remark,
                                    iatacode,
                                    awb_prefix,
                                    airline_code,
                                    create_by,
                                    update_by,
                                    create_timestamp,
                                    update_timestamp)
                                    values";
            return initialInser;
        }

        private static StringBuilder InsertToK35(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}),",
            "'" + r["CITY"] + "'", "'" + r["COUNTRY"] + "'", "'" + r["PARTYID"] + "'", "'" + TextHelper.Escape(r["FULLNAME"].ToString()) + "'", "'" + TextHelper.Escape(r["LOCALNAME"].ToString()) + "'",
            "'" + TextHelper.Escape(r["LSHORTNAME"].ToString()) + "'", "'" + r["DUPCHKKEY"] + "'", "'" + r["SALESCODE"] + "'",
             "'" + r["STATUS"] + "'", "'" + TextHelper.Escape(r["REMARK"].ToString()) + "'", "'" + r["IATACODE"] + "'", "'" + r["PREFIX"] + "'", "'" + r["CARRIERCODE"] + "'", "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion


    }
}
