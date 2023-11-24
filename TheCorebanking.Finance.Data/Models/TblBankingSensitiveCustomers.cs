using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingSensitiveCustomers
    {
        public long Id { get; set; }
        public string CustCode { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Dateccreated { get; set; }
        public bool? Approved { get; set; }
        public bool? Disapproved { get; set; }
        public string Approvedby { get; set; }
        public DateTime? DateApproved { get; set; }
        public string CustomerName { get; set; }
        public bool? Added { get; set; }
        public int? OperationId { get; set; }
    }
}
