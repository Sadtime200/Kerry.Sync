using Kerry.Sync.IMP.Common;
using System.Data;
using System.Text;

namespace Kerry.Sync.IMP.Factory
{
    public class CompanyVariantFactory:BaseFactory
    {

        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
        {
            return string.Format(@"
                      select ADDR.PARTYID COMPANYCODE,
                            ADDR.ADDRTYPE VARIANTTYPE,
                            ADDR.SNO,
                            '*' BIZTYPE,
                            '*' STATIONCODE,
                            P.FULLNAME,
                            ADDR1||chr(13)||ADDR2||chr(13)||ADDR3||chr(13)||ADDR4 ADDR,
                            ADDR.CITYCODE,
                            ADDR.CTRYCODE,
                            ADDR.STATEPROV,
                            ADDR.POSTALCODE,
                            ADDR.KNOWNTYPE,
                            ADDR.REGISTRATIONNO,
                            ADDR.EXPIRYDATE,
                            ADDR.CHECKDATE,
                            ADDR.CHECKBY
                            FROM FMPARTY P 
                            INNER JOIN FMPARTYADDR ADDR
                            ON P.UNID = ADDR.FMPARTY_UNID
                            WHERE P.STATUS = 'C' 
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
