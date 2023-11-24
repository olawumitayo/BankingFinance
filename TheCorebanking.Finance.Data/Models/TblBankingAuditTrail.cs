using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingAuditTrail
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string TransType { get; set; }
        public DateTime? TransDate { get; set; }
        public string TransTime { get; set; }
        public string Ipaddress { get; set; }
        public string CmpName { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
        public string DeptCode { get; set; }
        public bool? Status { get; set; }
    }
}
