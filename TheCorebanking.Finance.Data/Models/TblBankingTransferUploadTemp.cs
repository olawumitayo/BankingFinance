using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingTransferUploadTemp
    {
        public int Id { get; set; }
        public string AccountNoCr { get; set; }
        public string AccountNoDr { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? IsCheque { get; set; }
        public string ChequeNo { get; set; }
        public decimal? Amount { get; set; }
        public string NarrationDr { get; set; }
        public string NarrationCr { get; set; }
        public int? OperationType { get; set; }
        public int? TransType { get; set; }
        public decimal? AvailableBal { get; set; }
        public string DrName { get; set; }
        public string CrName { get; set; }
        public string DrStatus { get; set; }
        public string CrStatus { get; set; }
        public int ErrorCode { get; set; }
        public string BatchRef { get; set; }
        public bool IsDelete { get; set; }
    }
}
