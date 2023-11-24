using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingCamultipleTransfer
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string CustCode { get; set; }
        public string ProductAcctNo { get; set; }
        public string AccountName { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal? AmtTransfered { get; set; }
        public string Remark { get; set; }
        public bool Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public int? OperationId { get; set; }
        public bool Disapproved { get; set; }
        public string TransTime { get; set; }
        public string SlipNumber { get; set; }
        public string Narration { get; set; }
        public bool IsReversed { get; set; }
        public DateTime? ValueDate { get; set; }
        public string CreatedBy { get; set; }
        public decimal? FeeCharged { get; set; }
        public bool Amend { get; set; }
        public bool IsReverseAttempted { get; set; }
        public bool InEntry { get; set; }
        public bool IsCancel { get; set; }
    }
}
