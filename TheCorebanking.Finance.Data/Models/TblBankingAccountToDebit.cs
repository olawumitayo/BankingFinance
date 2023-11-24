using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingAccountToDebit
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal? Amount { get; set; }
        public string Reference { get; set; }
        public bool IsUsed { get; set; }
        public string SlipNo { get; set; }
        public int Transactiontype { get; set; }
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
    }
}
