using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
    public class ComapnyCreditTerm : BaseModel
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string StationCode { get; set; }
        public string BIZType { get; set; }
        public int CreditTermDays { get; set; }
        public decimal CreditLimit { get; set; }
        public string Currency { get; set; }
        public string OutOfCredit { get; set; }
    }
}
