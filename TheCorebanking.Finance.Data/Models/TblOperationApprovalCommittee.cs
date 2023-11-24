using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblOperationApprovalCommittee
    {
        public long Id { get; set; }
        public int? OperationId { get; set; }
        public string ApprovingAuthority { get; set; }
        public string StaffId { get; set; }
        public decimal? MaxAmt { get; set; }
        public decimal? MinAmt { get; set; }
        public string DesignationN { get; set; }
        public int? ApprovingLevel { get; set; }
        public string CompanyCode { get; set; }
        public string BranchCode { get; set; }
        public string Miscode { get; set; }
        public string CommitteeType { get; set; }
        public decimal? CummulativeAmt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
