using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class GeneralUploadTempRepository : EFRepository<TblBankingTransferUploadTemp>, IGeneralUploadTempRepository
    {
        public GeneralUploadTempRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingTransferUploadTemp> GeneralUploadTemp(int accountid)
        {
            return dbSet.Where(ps => ps.Id == accountid);
        }
        
    }
}
