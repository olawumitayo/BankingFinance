using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceBankReconciliationDetails
    {
        public int Id { get; set; }
        public string BankReconId { get; set; }
        public DateTime? ReconcilationDate { get; set; }
        public string Details { get; set; }
        public string Ref { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string TransactionId { get; set; }
        public bool? Matched { get; set; }
        public string MatchedNos { get; set; }
        public string Remark { get; set; }
        public string EntryBy { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public bool? Posted { get; set; }
        public string PostedBy { get; set; }
        public string Brcode { get; set; }
        public string Coycode { get; set; }
    }
}
