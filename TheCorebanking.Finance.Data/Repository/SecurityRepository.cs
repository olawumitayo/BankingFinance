using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class SecurityRepository : EFRepository<TblSecurityUsers>, ISecurityRepository
    {
        public SecurityRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblSecurityUsers> ValidateSecurity(string StaffNo)
        {
            return dbSet.Where(ps => ps.StaffNo == StaffNo);
        }

    }
}
