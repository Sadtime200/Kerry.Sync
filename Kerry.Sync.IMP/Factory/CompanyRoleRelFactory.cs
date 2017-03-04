using Kerry.Sync.IMP.Common;
using System.Data;
using System.Text;

namespace Kerry.Sync.IMP
{
    public class CompanyRoleRelFactory:BaseFactory
    {
        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
        {
            return string.Format(@"
                      SELECT R.PARTYID, C.DESCRIPTION FROM FMPARTYROLE R
                                INNER JOIN FMPARTY P 
                                ON R.FMPARTY_UNID = P.UNID
                                INNER JOIN FMCODE C
                                ON R.ROLE = C.CODE
                                WHERE P.STATUS = 'C' 
                                AND C.TYPE = 'PARTYROLE'
                        and P.PARTYID IN ({0})
                    ", sb.ToString());
        }

        public override string InitialInsertStr()
        {
            var initialInser = @"INSERT INTO tb_company_role_rel (COMPANY_ID,COMPANY_ROLE_ID,CREATE_BY,UPDATE_BY,CREATE_TIMESTAMP,UPDATE_TIMESTAMP) VALUES ";

            return initialInser;
        }

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5}),",
            "(select id from tb_company where company_code = '" + r["PARTYID"] + "' limit 1)", "(select id from tb_company_role where description = '" + r["DESCRIPTION"] + "' limit 1)"
            , "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion
    }
}
