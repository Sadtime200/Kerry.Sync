using Kerry.Sync.IMP.Common;
using Kerry.Sync.Utility.DB;
using Kerry.Sync.Utility.TaskManger;
using Kerry.Sync.Utility.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Kerry.Sync.IMP
{
  public  class CompanyRelFactory:BaseFactory
    {

        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
        {
            return string.Format(@"
                       SELECT R.PARTYID COMPANYCODE,
                              R.RELATION RELTYPE,
                              R.RELPARTYID RELCOMPANYCODE
                            FROM FMPARTY P
                            INNER JOIN FMPARTYREL R
                            ON P.UNID           = R.FMPARTY_UNID
                            AND R.FMPARTY_UNID IN
                              (SELECT UNID
                              FROM FMPARTY
                              WHERE STATUS = 'C'
                              )
                            WHERE P.STATUS = 'C'
                        and P.PARTYID IN ({0})
                    ", sb.ToString());
        }

        public override string InitialInsertStr()
        {
            var initialInser = @"insert ignore into tb_company_rel (company_id,rel_type,REL_COMPANY_ID,CREATE_BY,UPDATE_BY,CREATE_TIMESTAMP,UPDATE_TIMESTAMP) values  ";

            return initialInser;
        }

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5},{6}),",
            "(select id from tb_company where company_code = '" + r["COMPANYCODE"] + "' limit 1)", "'" + r["RELTYPE"] + "'", "(select id from tb_company where company_code = '" + r["RELCOMPANYCODE"] + "' limit 1)"
            , "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion
    }
}
