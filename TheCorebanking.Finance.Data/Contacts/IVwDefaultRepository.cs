using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IVwDefaultRepository : IRepository<FinanceBankingDefaultAccounts>
    {
        IQueryable<FinanceBankingDefaultAccounts> ValidateVw(int ID);
    }
}
