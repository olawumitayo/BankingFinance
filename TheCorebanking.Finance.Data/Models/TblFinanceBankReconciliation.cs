using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceBankReconciliation
    {
        public int Id { get; set; }
        public string BankReconId { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string BankAccNos { get; set; }
        public string BankGlaccNos { get; set; }
        public decimal? Glbalance { get; set; }
        public decimal? ClosingBnkStatementBal { get; set; }
        public decimal? OpeningBnkStatementBal { get; set; }
        public string EntryBy { get; set; }
        public bool? LastReconciliationDate { get; set; }
        public DateTime? ReconciliationDate { get; set; }
        public string Brcode { get; set; }
        public string Coycode { get; set; }
    }
}
