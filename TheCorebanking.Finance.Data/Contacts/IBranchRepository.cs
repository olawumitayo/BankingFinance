using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    
   public interface IBranchRepository : IRepository<TblBranchInformation>
    {
        IQueryable<TblBranchInformation> ValidateBranch(string companyId, int BranchID);

    }
}
