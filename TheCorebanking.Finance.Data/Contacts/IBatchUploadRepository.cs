using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IBatchUploadRepository : IRepository<TblBankingBatchTransferUploadTemp>
    {
        IQueryable<TblBankingBatchTransferUploadTemp> BatchUpload(int accountid);
    }
}
