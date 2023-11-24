using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblMultipleAccountToDebitFundTransfer
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public decimal? Amount { get; set; }
        public string Reference { get; set; }
        public bool IsUsed { get; set; }
        public int TransactionType { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public bool Approved { get; set; }
        public bool DisApprove { get; set; }
        public DateTime? DateApproved { get; set; }
        public string ApprovedBy { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public string Narration { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool IsCancel { get; set; }
        public int? OperationType { get; set; }
        public bool InEntryState { get; set; }
        public bool IsAmend { get; set; }
        public string TransCode { get; set; }
    }
}
