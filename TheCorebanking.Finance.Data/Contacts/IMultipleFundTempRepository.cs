using System.Linq;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IMultipleFundTempRepository : IRepository<TblMultipleAccountToCreditFundTransferTemp>
    {
        IQueryable<TblMultipleAccountToCreditFundTransferTemp> ValidateMultipleTemp(int accountid);

        //bool MultipleCredit(string reference, string accountNo, string branchId, string companyId, decimal amount, int transactionType, int operationType, string batchName,
        //   string narration, string approvedby, int operationID, string transCode, string valueDate, string dateCreated, string createdBy, bool inEntryState, int appSource, bool isJournal, bool isReciept, bool approved, bool disapproved);
    }
}
