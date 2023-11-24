using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
      public class StaffRepository : EFRepository<TblStaffInformation>, IStaffRepository
    {
        public StaffRepository(TheCoreBankingContext context) : base(context) { }

            public IQueryable<TblStaffInformation> ValidateStaff(long StaffId)
        {
            return dbSet.Where(ps => ps.Id == StaffId);
        }

    }
}
