using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class StatusRepository : EFRepository<TblFinanceStatus>, IStatusRepository
    {
        public StatusRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblFinanceStatus> ValidateStatus(int ID )
        {
            return dbSet.Where(ps => ps.StCode ==ID);
        }

    }
}
