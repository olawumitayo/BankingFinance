using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class AuditRepository : EFRepository<TblBankingAuditTrail>, IAuditRepository
    {
        public AuditRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingAuditTrail> ValidateAudit(int accountid)
        {
            return dbSet.Where(ps => ps.Id == accountid);
        }
        
    }
}
