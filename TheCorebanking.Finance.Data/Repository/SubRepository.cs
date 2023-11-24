using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class SubRepository : EFRepository<TblFinanceAccountSub>, ISubRepository
    {
        public SubRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblFinanceAccountSub> ValidateSub(long ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
