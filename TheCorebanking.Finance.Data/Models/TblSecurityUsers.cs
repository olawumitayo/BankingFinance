using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblSecurityUsers
    {
        public string StaffNumber { get; set; }
        public string StaffName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BranchCode { get; set; }
        public string CoyCode { get; set; }
        public string Miscode { get; set; }
        public string Department { get; set; }
        public bool? Registered { get; set; }
        public bool? Customer { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? NextPasswordChange { get; set; }
        public bool MultiBranch { get; set; }
        public bool MultiCompany { get; set; }
        public string StaffNo { get; set; }
        public bool? Disbursement { get; set; }
        public bool? InterestRateRevision { get; set; }
        public bool? AccountRestructure { get; set; }
        public bool? AccountSuspension { get; set; }
        public bool? AccountCancelation { get; set; }
        public bool? AccountTermination { get; set; }
        public bool? Payment { get; set; }
        public bool? CollateralManagement { get; set; }
        public bool? Reschedule { get; set; }
        public bool? AccountWithdrawal { get; set; }
        public bool? AccountDeposit { get; set; }
        public bool? AccountClose { get; set; }
        public bool? AccountFreeze { get; set; }
        public bool? AccountTransfer { get; set; }
        public bool? AddRecord { get; set; }
        public bool? DeleteRecord { get; set; }
        public bool? InterBranchPosting { get; set; }
        public bool? WalkOut { get; set; }
        public bool? Others { get; set; }
        public bool? AccountOpening { get; set; }
        public bool? Rediscount { get; set; }
        public bool? CallOperation { get; set; }
        public bool? Maturity { get; set; }
        public bool? Approval { get; set; }
        public bool? Servicess { get; set; }
        public bool? UserLogOn { get; set; }
        public bool? AllReports { get; set; }
        public bool? Casareports { get; set; }
        public bool? FinanceReports { get; set; }
        public bool? CapitalMarketReports { get; set; }
        public bool? TreasuryReports { get; set; }
        public bool? CustomerReports { get; set; }
        public bool? CreditReports { get; set; }
        public bool? UnitholdersReports { get; set; }
        public bool? OtherPayment { get; set; }
        public string TmpPasswordQuestion { get; set; }
        public string TmpPasswordAnswer { get; set; }
        public string TmpPassword { get; set; }
        public bool CanOverride { get; set; }
        public int SensitivityId { get; set; }
    }
}
