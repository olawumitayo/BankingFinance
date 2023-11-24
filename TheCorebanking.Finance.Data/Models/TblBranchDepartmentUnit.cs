using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBranchDepartmentUnit
    {
        public long Id { get; set; }
        public string CoyId { get; set; }
        public string BranchId { get; set; }
        public string Department { get; set; }
        public string DeptCode { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
    }
}
