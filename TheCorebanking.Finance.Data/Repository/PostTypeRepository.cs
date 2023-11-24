using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class PostTypeRepository : EFRepository<TblBankingPostType>, IPostTypeRepository
    {
        public PostTypeRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingPostType> ValidatePostType(int accountid)
        {
            return dbSet.Where(ps => ps.Id == accountid);
        }
        
    }
}
