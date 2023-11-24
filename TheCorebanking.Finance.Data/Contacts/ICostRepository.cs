using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface ICostRepository : IRepository<TblFinanceCostCenter>
    {
        IQueryable<TblFinanceCostCenter> ValidateCost(int ID);
    }
}
