using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblDepartment
    {
        public long Id { get; set; }
        public string CoyId { get; set; }
        public string Department { get; set; }
        public string Remark { get; set; }
        public string DeptCode { get; set; }

        public TblDepartment IdNavigation { get; set; }
        public TblDepartment InverseIdNavigation { get; set; }
    }
}
