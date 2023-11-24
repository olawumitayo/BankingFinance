using Microsoft.EntityFrameworkCore;
using System.Linq;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class AllBanksRepository : EFRepository<TblBankingAllBanks>, IAllBanksRepository
    {
        public AllBanksRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingAllBanks> ValidateBank(int BankId)
        {
            return dbSet.Where(ps => ps.BankId == BankId);
        }
       
    }
}
