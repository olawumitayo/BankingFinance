using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceTransaction
    {
        public int Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? TransactionType { get; set; }
        public string Description { get; set; }
        public string Ref { get; set; }
        public decimal? DebitAmt { get; set; }
        public decimal? CreditAmt { get; set; }
        public string AccountId { get; set; }
        public string PostedBy { get; set; }
        public string PostingTime { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public bool? Saved { get; set; }
        public string Sbu { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime ValueDate { get; set; }
        public string SourceBranch { get; set; }
        public string DestinationBranch { get; set; }
        public string Itemid { get; set; }
        public string Miscode { get; set; }
        public string Legtype { get; set; }
        public string BatchRef { get; set; }
        public string SCoyCode { get; set; }
        public string LcurrencyCode { get; set; }
        public double? CurrencyRate { get; set; }
        public string ApplicationId { get; set; }
        public string NonBrAccountId { get; set; }
    }
}
