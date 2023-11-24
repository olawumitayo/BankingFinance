using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;
using TheCoreBanking.Finance.Data.Repository;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class AccountCategoryRepository : EFRepository<TblFinanceAccountCategory>, IAccountCategoryRepository
    {
        public AccountCategoryRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblFinanceAccountCategory> ValidateCategory(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
