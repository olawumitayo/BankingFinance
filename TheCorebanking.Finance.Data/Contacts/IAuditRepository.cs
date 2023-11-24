using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IAuditRepository : IRepository<TblBankingAuditTrail>
    {
        IQueryable<TblBankingAuditTrail> ValidateAudit(int accountid);
    }
}
