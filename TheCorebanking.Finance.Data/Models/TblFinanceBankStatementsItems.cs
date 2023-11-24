using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceBankStatementsItems
    {
        public int Id { get; set; }
        public string BankReconId { get; set; }
        public DateTime? ReconciliationDate { get; set; }
        public string BankId { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string DebitAccountId { get; set; }
        public string CreditAccountId { get; set; }
        public string Remark { get; set; }
        public string EntryBy { get; set; }
        public bool? Posted { get; set; }
        public string Postedby { get; set; }
        public bool? Approved { get; set; }
        public string Approvedby { get; set; }
        public string Brcode { get; set; }
        public string Coycode { get; set; }
    }
}
