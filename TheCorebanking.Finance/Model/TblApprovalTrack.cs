using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Model
{
    public partial class TblApprovalTrack
    {
        public int Id { get; set; }
        public int? OperationId { get; set; }
        public int? TransactionId { get; set; }
        public string Coycode { get; set; }
        public string Brcode { get; set; }
        public int? ALevel { get; set; }
        public string Staffid { get; set; }
        public string Username { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string OperationName { get; set; }
        public DateTime? OperationDate { get; set; }
    }
}
