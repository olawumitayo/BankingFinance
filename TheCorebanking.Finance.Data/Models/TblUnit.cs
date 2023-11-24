using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblUnit
    {
        public long Id { get; set; }
        public string UnitName { get; set; }
        public string UnitCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
