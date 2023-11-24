using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingCatransfer
    {
        public int Id { get; set; }
        public string CustCode { get; set; }
        public string ProductAcctNo { get; set; }
        public string AccountName { get; set; }
        public string BranchId { get; set; }
        public string CoyCode { get; set; }
        public string Miscode { get; set; }
        public DateTime? DateCreated { get; set; }
        public string TransAcctName { get; set; }
        public string TransAcctNo { get; set; }
        public string TransBank { get; set; }
        public string TransBankAddr { get; set; }
        public string TransAcctType { get; set; }
        public decimal? AmtTransfered { get; set; }
        public string Remark { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public int? OperationId { get; set; }
        public bool? Disapproved { get; set; }
        public decimal? Balance { get; set; }
        public bool? Internal { get; set; }
        public decimal? TransAcctBal { get; set; }
        public string TransAcctCustCode { get; set; }
        public bool? SourceCa { get; set; }
        public bool? DestinationCa { get; set; }
        public string TransTime { get; set; }
        public string TillAcct { get; set; }
        public string PrincipalBalGl { get; set; }
        public bool? OtherSource { get; set; }
        public string SlipNumber { get; set; }
        public string Narration { get; set; }
        public bool? IsReversed { get; set; }
        public bool? SameCustomer { get; set; }
        public decimal? Charge { get; set; }
        public DateTime? ActualDate { get; set; }
        public string Comment { get; set; }
        public bool? IsCancel { get; set; }
        public bool? InEntryState { get; set; }
        public string AppId { get; set; }
        public string Narration2 { get; set; }
    }
}
