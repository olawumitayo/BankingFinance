using System.Linq;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IChargeTypeRepository : IRepository<TblTransferChargeType>
    {
        IQueryable<TblTransferChargeType> ValidateCharge(int ID);
    }
}
