using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class ChargeTypeRepository : EFRepository<TblTransferChargeType>, IChargeTypeRepository
    {
        public ChargeTypeRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblTransferChargeType> ValidateCharge(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
