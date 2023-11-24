using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblSingleFundTransfer
    {
        public long Id { get; set; }
        public string AccountDr { get; set; }
        public string AccountCr { get; set; }
        public decimal Amount { get; set; }
        public int? OperationType { get; set; }
        public int? TransactionType { get; set; }
        public int? OperationId { get; set; }
        public string CreateBy { get; set; }
        public string NarrationDr { get; set; }
        public string NarrationCr { get; set; }
        public string PostingTime { get; set; }
        public string Reference { get; set; }
        public string TransCode { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public string Remark { get; set; }
        public bool InEntryState { get; set; }
        public bool IsCancel { get; set; }
        public bool IsAmend { get; set; }
        public bool Approved { get; set; }
        public bool Disapproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool IsCheque { get; set; }
        public int AppSource { get; set; }
        public int? ApprovalLevel { get; set; }
        public string ChequeNo { get; set; }
        public bool IsJournal { get; set; }
        public bool IsReciept { get; set; }
        public string ReciepNo { get; set; }
        public bool IsRepayment { get; set; }
        public bool ChargeStamp { get; set; }
        public bool IsBatch { get; set; }
        public int TransferChargeType { get; set; }
    }
}
