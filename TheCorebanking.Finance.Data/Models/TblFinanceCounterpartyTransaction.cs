using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblFinanceCounterpartyTransaction
    {
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Ref { get; set; }
        public string CpId { get; set; }
        public string Description { get; set; }
        public int? DVolume { get; set; }
        public int? CVolume { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public string UserName { get; set; }
        public string TransactionType { get; set; }
        public int? AcctTransaction { get; set; }
        public string CustCode { get; set; }
        public string ProductCode { get; set; }
        public string Branch { get; set; }
        public string Coy { get; set; }
        public string FormNo { get; set; }
        public string BatchRef { get; set; }
        public DateTime? PostDate { get; set; }
        public string ApplicationId { get; set; }
        public bool? Approved { get; set; }
        public bool? Show { get; set; }
        public bool? IsReversed { get; set; }
        public string GlaccountId { get; set; }
        public bool? IsCleared { get; set; }
        public DateTime? SystemDateTime { get; set; }
        public string? OldAccountNo { get; set; }
        public string? Legtype { get; set; }
    }
}
