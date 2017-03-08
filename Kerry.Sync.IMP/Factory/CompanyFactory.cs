using Kerry.Sync.IMP.Common;
using Kerry.Sync.Utility.Text;
using System.Data;
using System.Text;

namespace Kerry.Sync.IMP
{
    public partial class CompanyFactory:BaseFactory
    {
        #region  Sql Part
        public override string GetK3Data(StringBuilder sb)
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

        public override string InitialInsertStr()
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

        public override StringBuilder InsertK3Data(StringBuilder insertStr, DataRow r)
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
