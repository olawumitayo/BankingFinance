using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    
    public class ChequeConfirmationRepository : EFRepository<TblBankingChequeConfirmation>, IChequeConfirmationRepository
    {
        //private readonly TheCoreBankingContext _theCoreBankingContext;
        public ChequeConfirmationRepository(TheCoreBankingContext context) : base(context) { } 

        public IQueryable<TblBankingChequeConfirmation> ValidateCheque(int ID)
        {
            return dbSet.Where(ps => ps.Id == ID);
        }
    }
}
