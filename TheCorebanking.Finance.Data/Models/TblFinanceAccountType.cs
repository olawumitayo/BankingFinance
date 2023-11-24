using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceAccountType
    {
        public long Id { get; set; }
        public int AccountCategoryId { get; set; }
        public string Description { get; set; }
       
        public int? BalanceSheetOrder { get; set; }
        public int? IncomeSheetOrder { get; set; }
        public string Active { get; set; }
        public int? SubCaptionId { get; set; }
        public string MainCaptionCode { get; set; }

        //public ICollection<TblFinanceChartOfAccount> chartOfAccounts { get; set; }
    }
}
