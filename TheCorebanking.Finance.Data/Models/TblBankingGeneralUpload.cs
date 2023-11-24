using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingGeneralUpload
    {
        public int Id { get; set; }
        public string Ref { get; set; }
        public string BatchRef { get; set; }
        public string AcctNo1 { get; set; }
        public string AcctNo2 { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ValueDate { get; set; }
        public bool IsCheque { get; set; }
        public string ChequeNo { get; set; }
        public string IsDebit { get; set; }
        public decimal Amount { get; set; }
        public string CrNarration { get; set; }
        public string DrNarration { get; set; }
        public bool IsCancel { get; set; }
        public string PostedBy { get; set; }
        public string PostingTime { get; set; }
        public bool Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public bool Disapproved { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public bool IsReversedAttempted { get; set; }
        public bool IsReversed { get; set; }
        public string IsReversedBy { get; set; }
        public string ReversedApprovedBy { get; set; }
        public DateTime? DateReversed { get; set; }
        public int OperationId { get; set; }
        public int TransId { get; set; }
        public bool InEntryState { get; set; }
        public string Comment { get; set; }
        public bool? IsAmend { get; set; }
    }
}
