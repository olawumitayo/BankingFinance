using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingReversal
    {
        public long Id { get; set; }
        public string AccountNo { get; set; }
        public string PostedBy { get; set; }
        public string ReversedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public string Comment { get; set; }
        public string Reference { get; set; }
        public string Remark { get; set; }
        public decimal AmountReversed { get; set; }
        public DateTime? DateReversed { get; set; }
        public DateTime? TrasactionDate { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? DateApproved { get; set; }
        public int? OperationId { get; set; }
        public int? TransactionId { get; set; }
        public bool Approved { get; set; }
        public bool Disapproved { get; set; }
        public bool IsAmend { get; set; }
    }
}
