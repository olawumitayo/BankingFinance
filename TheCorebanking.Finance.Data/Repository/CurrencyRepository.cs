using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;
using TheCorebanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class CurrencyRepository : EFRepository<TblFinanceCurrency>, ICurrencyRepository
    {
        public CurrencyRepository(TheCoreBankingContext context) : base(context) { }

        public IQueryable<TblFinanceCurrency> ValidateCurrency(int CurrencyId )
        {
            return dbSet.Where(ps => ps.CurrCode == CurrencyId);
        }

    }
}
