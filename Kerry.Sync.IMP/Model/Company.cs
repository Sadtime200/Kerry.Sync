using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
    public class Company : BaseModel
    {
        public int ID { get; set; }
        public string LocationCode { get; set; }
        public string CountryCode { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameLocal { get; set; }
        public string ShortName { get; set; }
        public string CompanyDupkey { get; set; }
        public string SalesUserCode { get; set; }
        public string Status { get; set; }
        public string LocalCurrency { get; set; }
        public string Remark { get; set; }
        public string IataCode { get; set; }
        public string IataAccno { get; set; }
        public string IataCassAddr { get; set; }
        public string IataParticipant { get; set; }
        public string PimasenderID { get; set; }
        public string Scaccode { get; set; }
        public string InttraSenderID { get; set; }
        public string AmsSenderID { get; set; }
        public string AciSenderID { get; set; }
        public string AfrSenderID { get; set; }
        public DateTime? LastUsedShp { get; set; }
        public DateTime? LastUsedCsgn { get; set; }
        public DateTime? LastUsedCust { get; set; }
        public DateTime? LastUserdFna { get; set; }
        public string AwbPrefix { get; set; }
        public string AirlineCode { get; set; }
        public string IsModifyMainInfo { get; set; }

    }
}
