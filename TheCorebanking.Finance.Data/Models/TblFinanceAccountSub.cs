using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceAccountSub
    {
        public long Id { get; set; }
        public string AccountSubId { get; set; }
        public string AccountSubName { get; set; }
        public string AccountId { get; set; }
        public int AccountStatus { get; set; }
        public int Currency { get; set; }
        public string UserName { get; set; }
        public string MsreplTranVersion { get; set; }
    }
}
