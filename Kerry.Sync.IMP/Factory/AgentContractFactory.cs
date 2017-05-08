using Kerry.Sync.IMP.Common;
using Kerry.Sync.IMP.Constants;
using Kerry.Sync.Utility.DB;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.Utility.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Factory
{
    public class AgentContractFactory : BaseFactory
    {
        #region Initial Region
        protected K5UATDBFactory _k5DB = new K5UATDBFactory();
        protected K35DEVDBFactory _k35 = new K35DEVDBFactory();
        protected TaskHelper _task = new TaskHelper();
        #endregion

        #region Implementation
        public override bool SynK3Data(out int k5Rows, out int k35Rows)
        {
            var sql = string.Empty;
            k35Rows = 0;
            k5Rows = 0;
            StringBuilder sb = new StringBuilder();
            StringBuilder insertStr = new StringBuilder();

            sql = GetK3Data(sb);
            var rows = _k5DB.ExecuteDataTable(sql).Rows;
            if (rows != null && rows.Count != 0)
            {
                k5Rows += rows.Count;
                insertStr = insertStr.Append(InitialInsertStr());
                foreach (DataRow r in rows)
                {
                    insertStr = InsertK3Data(insertStr, r);
                }
                var insertSql = insertStr.ToString().Remove(insertStr.Length - 1);

                k35Rows += _k35.ExecuteNonQuery(insertSql);
            }
            return true;
        }
        #endregion
        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
        {
            return string.Format(@"
                       select 
                            OAGENT,
                            BIZTYPE,
                            PARTYID_CSGN,
                            TO_CHAR(EFFDATE,'YYMMDD') EFFDATE ,
                             NVL(TO_CHAR(EXPIRYDATE,'YYMMDD'),'000000' ) EXPIRYDATE,
                            CURRCODE,
                            EXRATE,
                            PSRLA,
                            WGTUT,
                            REV_CODE,
                            COST_CODE,
                            PS_RATE_LOC1,
                            PS_RATE_LOC2,
                            PS_RATE_OA1,
                            PS_RATE_OA2,
                            BP1,
                            BP2,
                            BP3,
                            BP4,
                            BP5,
                            BP6,
                            BP7,
                            BP8,
                            BP9,
                            BP10,
                            REMARK,
                            CREATEBY,
                            CREATEDATE,
                            UPDATEBY,
                            UPDATEDATE,
                            PS_TYPE,
                            CALCFORM,
                            MHCOST,
                            SCHEMECODE,
                            PSCONNO,
                            COSTMARKUP,
                            COSTMARKUPTYPE,
                            CTNPERRATE,
                            CTNTYPE1,
                            CTNRATE1,
                            CTNTYPE2,
                            CTNRATE2,
                            CTNTYPE3,
                            CTNRATE3,
                            CTNTYPE4,
                            CTNRATE4,
                            CTNTYPE5,
                            CTNRATE5,
                            CTNTYPE6,
                            CTNRATE6,
                            TIMEZONE,
                            REV_STL_CHG,
                            COST_STL_CHG,
                            THIRDPARTYPS,
                            FOURTHPARTYPS,
                            PSRATELOCAL,
                            PSRATEOA,
                            PSRATETHIRD,
                            PSRATEFOURTH,
                            LCLRATETYPE,
                            LCLRATE,
                            STLDATETYPE,
                            LAGENT,
                            REV_STL_CHG_3RD,
                            COST_STL_CHG_3RD,
                            REV_STL_CHG_4TH,
                            COST_STL_CHG_4TH,
                            SETTLEMENTAGENT,
                            PSTYPEDESC,
                            PSCONTYPE
                            from fmagtcon
                    ", sb.ToString());
        }

        public override string InitialInsertStr()
        {
            var initialInser = @"insert ignore into tb_agent_contract
                                ( OAGENT_ID, BIZTYPE, CSGN_ID
            , EFFDATE, EXPIRYDATE, CURRCODE, EXRATE, PSRLA, WGTUT, REV_CODE, COST_CODE, PS_RATE_LOC1, PS_RATE_LOC2,
                                    PS_RATE_OA1, PS_RATE_OA2, BP1, BP2, BP3, BP4, BP5, BP6, BP7, BP8, BP9, BP10, REMARK, CREATEBY, 
CREATEDATE, UPDATEBY, UPDATEDATE, PS_TYPE

            , 
                                    CALCFORM, MHCOST, SCHEMECODE, PSCONNO, COSTMARKUP, COSTMARKUPTYPE, CTNPERRATE, CTNTYPE1, CTNRATE1, CTNTYPE2, CTNRATE2, CTNTYPE3, CTNRATE3,
                                    CTNTYPE4, CTNRATE4, CTNTYPE5, CTNRATE5, CTNTYPE6, CTNRATE6, TIMEZONE, REV_STL_CHG, COST_STL_CHG, THIRDPARTYPS, FOURTHPARTYPS, PSRATELOCAL, 
                                    PSRATEOA, PSRATETHIRD, PSRATEFOURTH, LCLRATETYPE, LCLRATE, STLDATETYPE, LAGENT, REV_STL_CHG_3RD, COST_STL_CHG_3RD, REV_STL_CHG_4TH, 
                                    COST_STL_CHG_4TH, SETTLEMENTAGENT, PSTYPEDESC, PSCONTYPE

) values 
";


            return initialInser;
        }

      

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format(@"({0},{1},{2},{ 3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}, {14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27} ,",
            //{28},{29},{30},{31},{32},{33},{34},{35})
            //,{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60},{61},{62},{63},{64},{65},{66},{67},{68},{69}),",


            "(select id from tb_company where company_code = '" + r["OAGENT"] + "' limit 1)", "'" + r["BIZTYPE"] + "'", "(select id from tb_company where company_code = '" + r["OAGENT"] + "' limit 1)", "DATE_FORMAT('" + r["EFFDATE"] + "','%Y-%m-%d')"
                     , "DATE_FORMAT('" + r["EFFDATE"] + "','%Y-%m-%d')", "DATE_FORMAT('" + r["EFFDATE"] + "','%Y-%m-%d')",
                     "'" + r["CURRCODE"] + "'", "'" + r["EXRATE"] + "'", "'" + r["PSRLA"] + "'", "'" + r["WGTUT"] + "'", "'" + r["REV_CODE"] + "'", "'" + r["COST_CODE"] + "'", "'" + r["PS_RATE_LOC1"] + "'", "'" + r["PS_RATE_LOC2"] + "'"
                     ,
                      "'" + r["PS_RATE_OA1"] + "'", "'" + r["PS_RATE_OA2"] + "'", "'" + r["BP1"] + "'", "'" + r["BP2"] + "'", "'" + r["BP3"] + "'", "'" + r["BP4"] + "'", "'" + r["BP5"] + "'", "'" + r["BP6"] + "'", "'" + r["BP7"] + "'",
                       "'" + r["BP8"] + "'", "'" + r["BP9"] + "'", "'" + r["BP10"] + "'", "'" + r["REMARK"] + "'", "'K3PATCH'"
                       , "sysdate()", "'K3PATCH'", "sysdate()", "'" + r["PS_TYPE"] + "'"
                     ,
                     "'" + r["CALCFORM"] + "'", "'" + r["MHCOST"] + "'", "'" + r["SCHEMECODE"] + "'", "'" + r["PSCONNO"] + "'"
                     //,
                     //  "'" + r["COSTMARKUP"] + "'", "'" + r["COSTMARKUPTYPE"] + "'", "'" + r["CTNPERRATE"] + "'", "'" + r["CTNTYPE1"] + "'", "'" + r["CTNRATE1"] + "'", "'" + r["CTNTYPE2"] + "'",
                     //   "'" + r["CTNRATE2"] + "'", "'" + r["CTNTYPE3"] + "'", "'" + r["CTNRATE3"] + "'", "'" + r["CTNTYPE4"] + "'", "'" + r["CTNRATE4"] + "'", "'" + r["CTNTYPE5"] + "'",
                     //    "'" + r["CTNRATE5"] + "'", "'" + r["CTNTYPE6"] + "'", "'" + r["CTNRATE6"] + "'", "'" + r["TIMEZONE"] + "'", "'" + r["REV_STL_CHG"] + "'", "'" + r["COST_STL_CHG"] + "'", "'" + r["THIRDPARTYPS"] + "'",
                     //     "'" + r["FOURTHPARTYPS"] + "'", "'" + r["PSRATELOCAL"] + "'", "'" + r["PSRATEOA"] + "'", "'" + r["PSRATETHIRD"] + "'", "'" + r["PSRATEFOURTH"] + "'", "'" + r["LCLRATETYPE"] + "'", "'" + r["LCLRATE"] + "'",
                     //      "'" + r["STLDATETYPE"] + "'", "'" + r["LAGENT"] + "'", "'" + r["REV_STL_CHG_3RD"] + "'", "'" + r["COST_STL_CHG_3RD"] + "'", "'" + r["REV_STL_CHG_4TH"] + "'", "'" + r["COST_STL_CHG_4TH"] + "'", "'" + r["SETTLEMENTAGENT"] + "'",
                            //"'" + r["PSTYPEDESC"] + "'", "'" + r["PSCONTYPE"] + "'"

                     )
                     );
            return insertStr;
        }
        #endregion

    }
}
