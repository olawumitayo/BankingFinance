using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceBonuses
    {
        public int BonusId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Symbol { get; set; }
        public string Security { get; set; }
        public decimal? Unit { get; set; }
        public decimal? ForEvery { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string Year { get; set; }
        public string MadeBy { get; set; }
    }
}
