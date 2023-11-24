using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceDefaultAccounts
    {
        public long DfId { get; set; }
        public string DfDescription { get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string FinancePnd { get; set; }
        public string FinancePnc { get; set; }
        public string TellerPnd { get; set; }
        public string CreatedBy { get; set; }
        public string CoyCode { get; set; }
        public string TellerPnc { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public bool? Disapproved { get; set; }
        public bool? Updated { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
