using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingSensitiveUsers
    {
        public long Id { get; set; }
        public string StaffNo { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Dateccreated { get; set; }
        public bool? Approved { get; set; }
        public bool? Disapproved { get; set; }
        public string Approvedby { get; set; }
        public DateTime? DateApproved { get; set; }
        public string UserName { get; set; }
        public bool? Added { get; set; }
        public int? OperationId { get; set; }
    }
}
