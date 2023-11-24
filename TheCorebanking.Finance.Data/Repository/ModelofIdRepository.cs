using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;
using TheCoreBanking.Finance.Data.Repository;

namespace TheCoreBanking.Fiance.Data.Repository
{
    
    public class ModelofIdRepository : EFRepository<TblBankingModeofId>, IModelofIdRepository
    {
        //private readonly TheCoreBankingContext _theCoreBankingContext;
        public ModelofIdRepository(TheCoreBankingContext context) : base(context) { } 

        public IQueryable<TblBankingModeofId> ValidateMode(int ID)
        {
            return dbSet.Where(ps => ps.Id == ID);
        }
    }
}
