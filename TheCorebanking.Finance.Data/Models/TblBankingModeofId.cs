using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingModeofId
    {
        public int Id { get; set; }
        public string Idmode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? Active { get; set; }
        public string Coy { get; set; }
    }
}
