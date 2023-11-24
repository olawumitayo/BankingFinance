using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface ISingleFundRepository : IRepository<TblSingleFundTransfer>
    {
        IQueryable<TblSingleFundTransfer> ValidateSingle(int ID);
    }
}
