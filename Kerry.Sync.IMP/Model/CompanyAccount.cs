using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
    public class CompanyAccount : BaseModel
    {
        public int ID { get; set; }
        public string CompanyCode { get; set; }
        public int CompanyID { get; set; }
        public int SNO { get; set; }
        public string BIZType { get; set; }
        public string Currency { get; set; }
        public string AccountType { get; set; }
        public string MapCode { get; set; }
        public string StationCode { get; set; }
    }
}
