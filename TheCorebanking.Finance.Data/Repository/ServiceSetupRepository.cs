using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class ServiceSetupRepository : EFRepository<TblServiceSetup>, IServiceSetupRepository
    {
        public ServiceSetupRepository(TheCoreBankingContext context) : base(context) { }

        public IQueryable<TblServiceSetup> ValidateService(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
