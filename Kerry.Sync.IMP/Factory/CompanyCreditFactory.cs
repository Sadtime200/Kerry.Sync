using Kerry.Sync.IMP.Common;
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
   public class CompanyCreditFactory:BaseFactory
    {

        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
        {
            return string.Format(@"
                                SELECT 
                                C.PARTYID COMPANYCODE,
                                C.OWNERID STATIONCODE,
                                C.BIZTYPE,
                                C.CRDDAY CREDITTERMDAYS,
                                C.CRDLIMIT CREDITLIMIT,
                                C.CRDCRNCY CURRCODE,
                                C.STATUS OUTOFCREDIT
                                FROM 
                                FMPARTY P INNER JOIN 
                                FMPARTYCREDITTERM C
                                ON P.UNID = C.FMPARTY_UNID
                                WHERE P.STATUS = 'C' 
                        and P.PARTYID IN ({0})
                    ", sb.ToString());
        }

        public override string InitialInsertStr()
        {
            var initialInser = @"INSERT IGNORE INTO TB_COMPANY_CREDITTERM 
                                (COMPANY_ID,STATION_CODE,BIZTYPE,CREDIT_TERM_DAYS,
                                CREDIT_LIMIT,CURRENCY,OUT_OF_CREDIT,CREATE_BY,UPDATE_BY,
                                CREATE_TIMESTAMP,UPDATE_TIMESTAMP) VALUES ";
            return initialInser;
        }

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}),",
            "(select id from tb_company where company_code = '" + r["COMPANYCODE"] + "' limit 1)", "'" + r["STATIONCODE"] + "'", "'" + r["BIZTYPE"] + "'", "'" + r["CREDITTERMDAYS"] + "'", "'" + r["CREDITLIMIT"] + "'",
            "'" + r["CURRCODE"] + "'", "'" + r["OUTOFCREDIT"] + "'", "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion

    }
}
