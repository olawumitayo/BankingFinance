using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblMisinformation
    {
        public int Id { get; set; }
        public string MisCode { get; set; }
        public string MisName { get; set; }
        public short? MisTypeId { get; set; }
        public string ParentMisCode { get; set; }
        public string CompanyCode { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
