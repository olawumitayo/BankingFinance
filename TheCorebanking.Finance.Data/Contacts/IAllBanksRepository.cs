using System.Linq;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IAllBanksRepository : IRepository<TblBankingAllBanks>
    {
        IQueryable<TblBankingAllBanks> ValidateBank(int BankId);
    }
}
