using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingCustomerPrivilege
    {
        public long Id { get; set; }
        public string GetUser { get; set; }
        public string CoyCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public bool? AccountBalance { get; set; }
        public bool? AccountTransaction { get; set; }
        public bool? AccountPrivilege { get; set; }
        public int? AccountPrivilegeLevel { get; set; }
        public bool? GlobalStatement { get; set; }
        public bool? GlobalBalance { get; set; }
    }
}
