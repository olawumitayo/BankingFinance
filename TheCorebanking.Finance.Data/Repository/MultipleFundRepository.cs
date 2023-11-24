using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class MultipleFundRepository : EFRepository<TblMultipleAccountToCreditFundTransfer>, IMultipleFundRepository
    {
        public MultipleFundRepository(TheCoreBankingContext context) : base(context) { }

        public IQueryable<TblMultipleAccountToCreditFundTransfer> ValidateMultiple(int accountid)
        {
            return dbSet.Where(ps => ps.Id == accountid);
        }
        //For Stored Procedure
        //public bool MultipleCredit(string reference, string accountNo, string branchId, string companyId ,decimal amount,int transactionType,int operationType,string batchName,
        //    string narration, string approvedby,int operationID,string transCode,string valueDate,string dateCreated,string createdBy,bool inEntryState,int appSource,bool isJournal,bool isReciept,bool approved,bool disapproved)
        //{
        //    var result = false;
        //    using (var context = new TheCoreBankingContext())
        //    {
        //        SqlParameter Reference = new SqlParameter("@Reference", reference);
        //        SqlParameter AccountNo = new SqlParameter("@AccountNo", accountNo);
        //        SqlParameter CompanyId = new SqlParameter("@CoyCode", companyId);
        //        SqlParameter BranchId = new SqlParameter("@BrCode", branchId);
        //        SqlParameter Amount = new SqlParameter("@Amount", amount);
        //        SqlParameter OperationID = new SqlParameter("@OperationID", operationID);
        //        SqlParameter Narration = new SqlParameter("@Narration", narration);
        //        SqlParameter TransCode = new SqlParameter("@TransCode", transCode);
        //        SqlParameter ValueDate = new SqlParameter("@ValueDate", valueDate);
        //        SqlParameter DateCreated = new SqlParameter("@DateCreated", dateCreated);
        //        SqlParameter CreatedBy = new SqlParameter("@CreatedBy", createdBy);
        //        SqlParameter InEntryState = new SqlParameter("@InEntryState", inEntryState);
        //        SqlParameter TransactionType = new SqlParameter("@TransactionType", transactionType);
        //        SqlParameter OperationType = new SqlParameter("@OperationType", operationType);
        //        SqlParameter AppSource = new SqlParameter("@AppSource", appSource);
        //        SqlParameter BatchName = new SqlParameter("@BatchName", batchName);
        //        SqlParameter IsReciept = new SqlParameter("@IsReciept", isReciept);
        //        SqlParameter IsJournal = new SqlParameter("@IsJournal", isJournal);
        //        SqlParameter Approved = new SqlParameter("@Approved", approved);
        //        SqlParameter Disapproved = new SqlParameter("@Disapproved", disapproved);

        //        context.Database.ExecuteSqlCommand("dbo.InsertCreditMultiple_FundTransfer @Reference, @AccountNo, @BrCode,@CoyCode,@Amount,@OperationID,@Narration,@TransCode,@ValueDate,@DateCreated,@CreatedBy,@InEntryState,@TransactionType,@OperationType,@BatchName,@AppSource,@IsJournal,@IsReciept,@Approved,@Disapproved",
        //            Reference, AccountNo, BranchId, CompanyId, Amount, OperationID, Narration, TransCode, ValueDate, DateCreated, CreatedBy, InEntryState,TransactionType,OperationType, BatchName, AppSource, IsJournal, IsReciept, Approved, Disapproved);

        //        result = true;
        //    }
        //    return result;
        //}

    }
}
