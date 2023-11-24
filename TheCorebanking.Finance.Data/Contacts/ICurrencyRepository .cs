using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    
   public interface ICurrencyRepository : IRepository<TblFinanceCurrency>
    {
        IQueryable<TblFinanceCurrency> ValidateCurrency(int CurrencyID);

    }
}
