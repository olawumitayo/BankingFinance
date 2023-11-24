using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IStatusRepository : IRepository<TblFinanceStatus>
    {
        IQueryable<TblFinanceStatus> ValidateStatus(int ID);
    }
}
