using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class AccountGroupRepository : EFRepository<TblFinanceAccountGroup>, IAccountGroupRepository
    {
        public AccountGroupRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblFinanceAccountGroup> ValidateGroup(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
