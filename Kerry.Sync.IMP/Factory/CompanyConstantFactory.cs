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
    public class CompanyConstantFactory:BaseFactory
    {

        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
        {
            return string.Format(@"
                       SELECT  
                                C.PARTYID COMPANYCODE,
                                C.BIZTYPE ,
                                C.OWNERID  STATIONCODE,
                                C.CONTYPE CONSTANTTYPE,
                                C.VALUE
                                FROM 
                                FMPARTY P INNER JOIN
                                FMPARTYCONSTANT C
                                ON P.UNID = C.FMPARTY_UNID
                                WHERE P.STATUS = 'C' 
                        and P.PARTYID IN ({0})
                    ", sb.ToString());
        }

        public override string InitialInsertStr()
        {
            var initialInser = @"insert ignore into tb_company_constant 
                                (company_id,biztype,STATION_CODE,CONSTANT_TYPE,value,CREATE_BY,
                                UPDATE_BY,CREATE_TIMESTAMP,UPDATE_TIMESTAMP) values ";

            return initialInser;
        }

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8}),",
            "(select id from tb_company where company_code = '" + r["COMPANYCODE"] + "' limit 1)", "'" + r["BIZTYPE"] + "'", "'" + r["STATIONCODE"] + "'", "'" + r["CONSTANTTYPE"] + "'", "'" + r["VALUE"] + "'",
             "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion


    }
}
