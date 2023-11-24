using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    
   public interface ITillLimitRepository : IRepository<TblTellersetup>
    {
        IQueryable<TblTellersetup> ValidateTill(long companyId);

    }
}
