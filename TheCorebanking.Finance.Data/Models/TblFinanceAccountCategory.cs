using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceAccountCategory
    {
        public long Id { get; set; }
        public string Descriptions { get; set; }
        public int AccountGroupId { get; set; }
        public string Active { get; set; }
    }
}
