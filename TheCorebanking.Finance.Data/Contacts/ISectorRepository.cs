using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface ISectorRepository:IRepository<TblBankingSector>
    {
        IQueryable<TblBankingSector> ValidateSector(int ID);
    }
}
