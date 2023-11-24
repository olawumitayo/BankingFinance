using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface ICustomerandGLRepository : IRepository<vw_CustomerAndGLAccount>
    {
        //IQueryable<TblCasa> ValidateAccount(int accountid);
    }
}
