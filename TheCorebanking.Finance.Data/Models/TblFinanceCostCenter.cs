using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceCostCenter
    {
        public long Id { get; set; }
        public int Costcode { get; set; }
        public string Costname { get; set; }
    }
}
