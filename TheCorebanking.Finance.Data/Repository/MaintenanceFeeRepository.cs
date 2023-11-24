using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class MaintenanceFeeRepository : EFRepository<TblBankingCottransaction>, IMaintenanceFeeRepository
    {
        public MaintenanceFeeRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingCottransaction> ValidateCot(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
