using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class ApprovalTrack2
    {
        public int Id { get; set; }
        public int? Opid { get; set; }
        public int? Transid { get; set; }
        public string Coycode { get; set; }
        public string Brcode { get; set; }
        public int? ALevel { get; set; }
        public string Staffid { get; set; }
        public string Username { get; set; }
        public decimal? Mins { get; set; }
        public decimal? Maxs { get; set; }
        public string OpName { get; set; }
    }
}
