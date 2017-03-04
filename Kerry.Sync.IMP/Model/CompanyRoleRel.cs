using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
    public class CompanyRoleRel : BaseModel
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyRole { get; set; }
        public int CompanyRoleID { get; set; }
    }
}
