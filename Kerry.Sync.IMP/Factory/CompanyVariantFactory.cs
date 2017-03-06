using Kerry.Sync.IMP.Common;
using Kerry.Sync.Utility.Text;
using System.Data;
using System.Text;

namespace Kerry.Sync.IMP
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
                            case when nvl(to_char(ADDR.EXPIRYDATE,'YYMMDD'),'201010')<'371231' then NVL(TO_CHAR(ADDR.EXPIRYDATE,'YYMMDD'),'000000' ) else '371231' end  EXPIRYDATE,
                            case when nvl(to_char(ADDR.CHECKDATE,'YYMMDD'),'201010')<'371231' then NVL(TO_CHAR(ADDR.CHECKDATE,'YYMMDD'),'000000' ) else '371231' end  CHECKDATE,
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
            var initialInser = @"
set foreign_key_checks=0;
insert ignore into tb_company_variant 
                                    (company_id,
                                    COMPANY_VARIANT_TYPE_ID,
                                    sno,
                                    biztype,
                                    STATION_CODE,
                                    COMPANY_VAR_NAME,
                                    ADDR,
                                    location_code,
                                    country_code,
                                    STATE_PROVINCE,
                                    POSTAL_CODE,
                                    KNOWN_TYPE,
                                    KNOWN_REGNO,
                                    KNOWN_EXPIRY,
                                    KNOWN_CHECK,
                                    KNOWN_CHECKBY,
                                    CREATE_BY,UPDATE_BY,CREATE_TIMESTAMP,UPDATE_TIMESTAMP
                                    ) values";

            return initialInser;
        }

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
        {
            insertStr = insertStr.Append(string.Format("({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}),",
            "(select id from tb_company where company_code = '" + r["COMPANYCODE"] + "' limit 1)", "(select id from tb_company_variant_type where variant_type = '" + r["VARIANTTYPE"] + "' limit 1)", r["sno"] 
            , "'" + r["biztype"] + "'", "'" + r["STATIONCODE"] + "'", "'" + TextHelper.Escape(r["FULLNAME"].ToString()) + "'", "'" + TextHelper.Escape(r["ADDR"].ToString()) + "'", "'" + r["CITYCODE"] + "'"
            , "'" + r["CTRYCODE"] + "'", "'" + r["STATEPROV"] + "'", "'" + r["POSTALCODE"] + "'", "'" + r["KNOWNTYPE"] + "'", "'" + r["REGISTRATIONNO"] + "'", "DATE_FORMAT('" + r["EXPIRYDATE"] + "','%Y-%m-%d')", "DATE_FORMAT('" + r["CHECKDATE"] + "','%Y-%m-%d')"
            , "'" + r["CHECKBY"] + "'" , "'K3PATCH'", "'K3PATCH'", "sysdate()", "sysdate()"));
            return insertStr;
        }
        #endregion
    }
}
