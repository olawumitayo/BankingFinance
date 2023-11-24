using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
 
    public interface IStaffRepository : IRepository<TblStaffInformation>
    {
        IQueryable<TblStaffInformation> ValidateStaff(long StaffId);

    }
}
