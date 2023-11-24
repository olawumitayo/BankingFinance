using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
using TheCorebanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IReversalRepository : IRepository<TblBankingReversal>
    {
        IQueryable<TblBankingReversal> ValidateReversal(int ID);
    }
}
