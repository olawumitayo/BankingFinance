using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblProductGroup
    {
        public long Id { get; set; }
        public string Productgroupid { get; set; }
        public string Productgroupcode { get; set; }
        public string Productgroupname { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Datetimecreated { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? Datetimedeleted { get; set; }
    }
}
