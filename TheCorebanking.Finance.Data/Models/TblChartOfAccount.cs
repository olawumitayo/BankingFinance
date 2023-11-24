using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblChartOfAccount
    {
        public long Id { get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountTypeId { get; set; }
        public string ProductId { get; set; }
        public string CurrencyId { get; set; }
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public string LevelId { get; set; }
        public bool? IsParentGl { get; set; }
        public string ParentGlid { get; set; }
        public bool? IncomeSheetOrder { get; set; }
        public bool? BalanceSheetOrder { get; set; }
        public bool? AccountStatus { get; set; }
        public bool? BranchSpecific { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
