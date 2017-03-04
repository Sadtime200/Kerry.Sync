using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.Sync.IMP.Model
{
    public class BaseModel
    {
        public string CreateBy { get; set; }
        public DateTime CreateTimesstamp { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}
