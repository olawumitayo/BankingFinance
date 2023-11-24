using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class GeneralUploadRepository : EFRepository<TblBankingGeneralUpload>, IGeneralUploadRepository
    {
        public GeneralUploadRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingGeneralUpload> GeneralUpload(int accountid)
        {
            return dbSet.Where(ps => ps.Id == accountid);
        }
        
    }
}
