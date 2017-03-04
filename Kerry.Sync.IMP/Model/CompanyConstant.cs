using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
    public class CompanyConstant : BaseModel
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string BIZType { get; set; }
        public string StationCode { get; set; }
        public string ConstantType { get; set; }
        public string Value { get; set; }
    }
}
