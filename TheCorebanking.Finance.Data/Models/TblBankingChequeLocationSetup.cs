using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingChequeLocationSetup
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int? Value { get; set; }
        public decimal? ReturnCharge { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
    }
}
