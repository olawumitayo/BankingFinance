using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingAllBanks
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string Oldname { get; set; }
        public string BranchName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNo { get; set; }
        public string ContactEmail { get; set; }
        public string ContactAddress { get; set; }
    }
}
