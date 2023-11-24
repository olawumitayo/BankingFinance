using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class BranchRepository : EFRepository<TblBranchInformation>, IBranchRepository
    {
        public BranchRepository(TheCoreBankingContext context) : base(context) { }

        public IQueryable<TblBranchInformation> ValidateBranch(string companyId, int BranchId )
        {
            return dbSet.Where(ps => ps.CoyId == companyId &&  ps.Id == BranchId);
        }

    }
}
