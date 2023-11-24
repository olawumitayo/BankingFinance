using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingChequeConfirmation
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public decimal? Amount { get; set; }
        public string BeneficiaryName { get; set; }
        public string ChequeNo { get; set; }
        public string ConfirmedFrom { get; set; }
        public string PnoneNumber { get; set; }
        public string ConfirmedBy { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? DateConfirmed { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
    }
}
