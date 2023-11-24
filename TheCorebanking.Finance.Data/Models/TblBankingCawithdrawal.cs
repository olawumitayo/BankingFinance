using System;
using System.Collections.Generic;
using System.Text;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblBankingCawithdrawal
    {
        public int Id { get; set; }
        public string CustCode { get; set; }
        public string ProductAcctNo { get; set; }
        public string AccountName { get; set; }
        public string BranchId { get; set; }
        public string CoyCode { get; set; }
        public string Miscode { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Name { get; set; }
        public string Addresss { get; set; }
        public decimal? AmtWithdraw { get; set; }
        public string Remark { get; set; }
        public bool? Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }
        public int? OperationId { get; set; }
        public string SlipNumber { get; set; }
        public decimal? Balance { get; set; }
        public bool? Disapproved { get; set; }
        public string Chequeno { get; set; }
        public bool? SourceCa { get; set; }
        public bool? DestinationCa { get; set; }
        public string TransTime { get; set; }
        public string TillAcct { get; set; }
        public string PrincipalBalGl { get; set; }
        public string CustomerBr { get; set; }
        public bool? Cheque { get; set; }
        public string Narration { get; set; }
        public string UploadDoc { get; set; }
        public bool? IsReversed { get; set; }
        public DateTime? ActualDate { get; set; }
        public bool? ForceDr { get; set; }
        public string FormId { get; set; }
        public string UserName { get; set; }
        public bool? IsCounterCheque { get; set; }
        public bool ChargeStamp { get; set; }
    }
}
