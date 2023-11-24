using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class TillLimitRepository : EFRepository<TblTellersetup>, ITillLimitRepository
    {
        public TillLimitRepository(TheCoreBankingContext context) : base(context) { }

        public IQueryable<TblTellersetup> ValidateTill(long companyId)
        {
            return dbSet.Where(ps => ps.Companyid == companyId.ToString() );
        }

    }
}
