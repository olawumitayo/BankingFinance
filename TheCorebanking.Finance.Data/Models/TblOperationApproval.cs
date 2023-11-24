using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblOperationApproval
    {
        public int Id { get; set; }
        public int? OperationId { get; set; }
        public string ApprovingAuthority { get; set; }
        public string StaffId { get; set; }
        public decimal? MaxAmt { get; set; }
        public decimal? MinAmt { get; set; }
        public string Designation { get; set; }
        public int? ApprovingLevel { get; set; }
        public string CompanyCode { get; set; }
        public string BranchCode { get; set; }
        public string Miscode { get; set; }
        public string CommitteeType { get; set; }
        public decimal? CummulativeAmt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Ref { get; set; }
        public int? OpId { get; set; }
        public bool? Approved { get; set; }
        public bool? Disapproved { get; set; }
        public string Approvedby { get; set; }
        public DateTime? DateApproved { get; set; }
        public string Comment { get; set; }
    }
}
