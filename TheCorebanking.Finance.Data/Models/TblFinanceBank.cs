using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceBank
    {
        public int Id { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string AccNo { get; set; }
        public string BranchName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNo { get; set; }
        public string ContactEmail { get; set; }
        public string AccountId { get; set; }
        public string ContactAddress { get; set; }
    }
}
