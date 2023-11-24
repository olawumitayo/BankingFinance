using Microsoft.EntityFrameworkCore;
using System.Linq;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class CasaRepository : EFRepository<TblCasa>, ICasaRepository
    {
        public CasaRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblCasa> ValidateAccount(int accountid)
        {
            return dbSet.Where(ps => ps.Casaaccountid == accountid);
        }
        
    }
}
