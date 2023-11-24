using System.Linq;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface ICasaRepository : IRepository<TblCasa>
    {
        IQueryable<TblCasa> ValidateAccount(int accountid);
    }
}
