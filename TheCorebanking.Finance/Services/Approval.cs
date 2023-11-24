using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Services
{
    public class Approval
    {
        public int OpId { get; set; }
        public int transId { get; set; }
        public string CoyCode { get; set; }
        public string brcode { get; set; }
        public int aLevel { get; set; }
        public string staffid { get; set; }
        public string username { get; set; }
        public decimal min { get; set; }
        public string Opname { get; set; }
        public decimal max { get; set; }
    }
}
