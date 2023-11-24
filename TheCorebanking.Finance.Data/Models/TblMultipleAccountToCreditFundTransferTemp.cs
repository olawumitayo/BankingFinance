using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblMultipleAccountToCreditFundTransferTemp
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string AccountNo { get; set; }
        public string AccountNoDr { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public decimal? Amount { get; set; }
        public string Remark { get; set; }
        public string ApprovedBy { get; set; }
        public int? OperationId { get; set; }
        public string Narration { get; set; }
        public string TransCode { get; set; }
        public DateTime? ValueDate { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public bool Approved { get; set; }
        public bool Disapproved { get; set; }
        public bool IsAmend { get; set; }
        public bool InEntryState { get; set; }
        public bool IsCancel { get; set; }
        public int TransactionType { get; set; }
        public int OperationType { get; set; }
        public string BatchName { get; set; }
        public int AppSource { get; set; }
        public bool IsJournal { get; set; }
        public bool IsReciept { get; set; }
        public string ReciepNo { get; set; }
        public decimal TotalDebit { get; set; }
    }
}
