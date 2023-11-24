using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class DefaultAccountRepository : EFRepository<TblFinanceDefaultAccounts>, IDefaultAccountRepository
    {
        public DefaultAccountRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblFinanceDefaultAccounts> ValidateDefault(int ID )
        {
            return dbSet.Where(ps => ps.DfId ==ID);
        }

    }
}
