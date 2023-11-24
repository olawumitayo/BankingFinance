using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingBatchTransferUploadTemp
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public bool IsCheque { get; set; }
        public string ChequeNo { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountName { get; set; }
        public string Status { get; set; }
        public int ErrorCode { get; set; }
        public string BatchRef { get; set; }
        public bool IsDelete { get; set; }
        public bool StampDuty { get; set; }
    }
}
