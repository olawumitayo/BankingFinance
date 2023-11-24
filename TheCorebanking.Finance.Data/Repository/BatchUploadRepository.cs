using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class BatchUploadRepository : EFRepository<TblBankingBatchTransferUploadTemp>, IBatchUploadRepository
    {
        public BatchUploadRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBankingBatchTransferUploadTemp> BatchUpload(int accountid)
        {
            return dbSet.Where(ps => ps.Id == accountid);
        }
        
    }
}
