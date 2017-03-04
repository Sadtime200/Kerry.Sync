using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
 
        public class CompanyVariant : BaseModel
        {
            public int ID { get; set; }
            public int CompanyID { get; set; }
            public string CompanyCode { get; set; }
            public int CompanyVariantTypeID { get; set; }
            public string CompanyVariantType { get; set; }
            public int SNO { get; set; }
            public string BIZType { get; set; }
            public string StationCode { get; set; }
            //public string TAXPAYER_REG_NO { get; set; }
            public string CompanyName { get; set; }
            public string Addr { get; set; }
            public string LocationCode { get; set; }
            public string CountryCode { get; set; }
            public string StateProvince { get; set; }
            public string PostalCode { get; set; }
            //public string TEL { get; set; }
            //public string FAX { get; set; }
            //public string CTC { get; set; }
            public string KnownType { get; set; }
            public string KnownRegon { get; set; }
            public Nullable<System.DateTime> KnownExpiry { get; set; }
            public Nullable<System.DateTime> KnownCheck { get; set; }
            public string KnownCheckBy { get; set; }

        }
}
