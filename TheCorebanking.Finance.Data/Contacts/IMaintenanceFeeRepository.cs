using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IMaintenanceFeeRepository : IRepository<TblBankingCottransaction>
    {
        IQueryable<TblBankingCottransaction> ValidateCot(int ID);
    }
}
