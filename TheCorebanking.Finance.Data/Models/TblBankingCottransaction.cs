using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingCottransaction
    {
        public int Id { get; set; }
        public string CustCode { get; set; }
        public string ProductAcctNo { get; set; }
        public int OperationId { get; set; }
        public decimal Cotamount { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
        public bool? SameCustomer { get; set; }
        public bool? SameAccount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Narration { get; set; }
        public string PostedBy { get; set; }
        public bool? IsCharged { get; set; }
        public int? PdId { get; set; }
        public int? CutOffDay { get; set; }
        public string ReferenceId { get; set; }
        public bool? Internal { get; set; }
    }
}
