using System;
using TheCorebanking.Finance.Data;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data
{
    public class FinanceDataModel
    {
       public TblFinanceAccountCategory categorys { get; set; }

       public TblFinanceAccountGroup groups { get; set; }

        public TblFinanceAccountType types { get; set; }
       
        public TblFinanceChartOfAccount charts { get; set; }
    }
}
