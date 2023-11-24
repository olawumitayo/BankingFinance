using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IPostTypeRepository : IRepository<TblBankingPostType>
    {
        IQueryable<TblBankingPostType> ValidatePostType(int accountid);
    }
}
