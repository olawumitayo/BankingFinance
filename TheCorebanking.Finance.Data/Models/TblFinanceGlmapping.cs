using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceGlmapping
    {
        public int Id { get; set; }
        public string FinTrakGl { get; set; }
        public string FinTrakGlcode { get; set; }
        public string OtherGl { get; set; }
        public string OtherGlcode { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
