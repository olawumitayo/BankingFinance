using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using TheCorebanking.Finance.Data;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class CounterpartyRepository : EFRepository<TblFinanceCounterpartyTransaction>, ICounterpartyRepository
    {
        public CounterpartyRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblFinanceCounterpartyTransaction> ValidateCounterparty(int id)
        {
            return dbSet.Where(ps => ps.TransactionId == id);
        }
        //For Stored Procedure
        public decimal SPGLBalance(string accountNo)
        {
            var GLBalance = new GLBalance();
            using (var context = new TheCoreBankingContext())
            {
                SqlParameter AcctNo = new SqlParameter("@AccountID", accountNo);

                GLBalance = context.GLBalance.FromSqlRaw("General.GetGLBalance2 @AccountID", AcctNo).Single();
                //context.Database.ExecuteSqlRaw("GetGLBalance @AccountID", accountNo);


            }
            return Convert.ToDecimal(GLBalance.GLBalances);
        }
        public decimal CustomerBalance(string accountNo)
        {
            try
            {
                var GetCustomerBalance = new GetCustomerBalance();
                using (var context = new TheCoreBankingContext())
                {
                    SqlParameter AcctNo = new SqlParameter("@AccountNumber", accountNo);
                    //decimal? Balance = 0;
                    GetCustomerBalance = context.GetCustomerBalance.FromSqlRaw("dbo.GetCustomerBalance @AccountNumber", AcctNo).FirstOrDefault();
                    return GetCustomerBalance.GetCustomerBalances;

                }

            }
            catch (Exception ex)
            {

                return 0;
            }


        }
        public List<sp_MultipleandSingle> spMultipleSingle(string TranDate, string initiators)
        {
            List<sp_MultipleandSingle> multipleSingle = new List<sp_MultipleandSingle>();
            try
            {

                using (var context = new TheCoreBankingContext())
                {
                    SqlParameter date = new SqlParameter("@date", TranDate);
                    SqlParameter initiator = new SqlParameter("@initiator", initiators);
                    return context.sp_MultipleandSingle.FromSqlRaw("dbo.sp_MultipleandSingle @date,@initiator", date, initiator).ToList();
                    //return multipleSingle;
                }
            }
            catch (Exception ex)
            {

                return multipleSingle;
            }
        }
        public List<sp_Single> spSingle(string TranDate, string initiators)
        {
            List<sp_Single> Single = new List<sp_Single>();
            try
            {

                using (var context = new TheCoreBankingContext())
                {
                    SqlParameter date = new SqlParameter("@date", TranDate);
                    SqlParameter initiator = new SqlParameter("@initiator", initiators);
                    return context.sp_Single.FromSqlRaw("dbo.sp_Single @date,@initiator", date, initiator).ToList();
                    //return multipleSingle;
                }
            }
            catch (Exception ex)
            {

                return Single;
            }
        }
        public bool PostTransaction(string CrAccount, string DrAccount, string BrCode, string CoyCode, string MISCode, decimal Amount, int TransactionType, string CrNarration, string DrNarration, string PostedBy, string AppID, DateTime ValueDate, string BatchRef, string transSequence, bool Status)
        {
            var PostingDTO = new PostingDTO();
            transSequence = string.Empty;
            var result = false;
            transSequence = new Random().Next().ToString().Substring(0, 7);
            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _CrAccount = new SqlParameter("@crACCountNo", CrAccount);
                SqlParameter _DrAccount = new SqlParameter("@drACCountNo", DrAccount);
                SqlParameter _BrCode = new SqlParameter("@brCode", BrCode);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", GetCompanyCode());
                SqlParameter _MISCode = new SqlParameter("@MISCode", MISCode);
                SqlParameter _Amount = new SqlParameter("@Amount", Amount);
                SqlParameter _TransactionType = new SqlParameter("@TransactionType", TransactionType);
                SqlParameter _CrNarration = new SqlParameter("@NarrationCr", CrNarration);
                SqlParameter _DrNarration = new SqlParameter("@NarrationDr", DrNarration);
                SqlParameter _PostedBy = new SqlParameter("@PostedBy", PostedBy);
                SqlParameter _AppID = new SqlParameter("@ApplicationID", AppID);
                SqlParameter _ValueDate = new SqlParameter("@ValueDate", ValueDate);
                SqlParameter _BatchRef = new SqlParameter("@BatchRef", BatchRef);
                SqlParameter _transSequence = new SqlParameter("@TransSequence", transSequence);
                SqlParameter _Status = new SqlParameter("@Status", Status);
                context.Database.ExecuteSqlRaw("InsertCustomerToCustomer_FinTrak_Service @crACCountNo,@drACCountNo, @brCode,@CoyCode,@MISCode,@Amount,@TransactionType,@NarrationCr,NarrationDr,@PostedBy,@ApplicationID,@ValueDate,@BatchRef,@TransSequence,@Status", _CrAccount, _DrAccount, _BrCode, _CoyCode, _MISCode, _Amount, _TransactionType, _CrNarration, _DrNarration, _PostedBy, _AppID, _ValueDate, _BatchRef, _transSequence, _Status);

                result = true;
            }
            return result;
        }

        public bool PostTransactions(string CrAccount, string DrAccount, string BrCode, string CoyCode, string MISCode, decimal Amount, int TransactionType, string CrNarration, string DrNarration, string PostedBy, string AppID, DateTime ValueDate, string BatchRef, string transSequence, bool Status)
        {
            var PostingDTO = new PostingDTO();
            transSequence = string.Empty;
            var result = false;
            transSequence = new Random().Next().ToString().Substring(0, 7);
            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _CrAccount = new SqlParameter("@crACCountNo", CrAccount);
                SqlParameter _DrAccount = new SqlParameter("@drACCountNo", DrAccount);
                SqlParameter _BrCode = new SqlParameter("@brCode", BrCode);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", GetCompanyCode());
                SqlParameter _MISCode = new SqlParameter("@MISCode", MISCode);
                SqlParameter _Amount = new SqlParameter("@Amount", Amount);
                SqlParameter _TransactionType = new SqlParameter("@TransactionType", TransactionType);
                SqlParameter _CrNarration = new SqlParameter("@NarrationCr", CrNarration);
                SqlParameter _DrNarration = new SqlParameter("@NarrationDr", DrNarration);
                SqlParameter _PostedBy = new SqlParameter("@PostedBy", PostedBy);
                SqlParameter _AppID = new SqlParameter("@ApplicationID", AppID);
                SqlParameter _ValueDate = new SqlParameter("@ValueDate", ValueDate);
                SqlParameter _BatchRef = new SqlParameter("@BatchRef", BatchRef);
                SqlParameter _transSequence = new SqlParameter("@TransSequence", transSequence);
                SqlParameter _Status = new SqlParameter("@Status", Status);

                context.Database.ExecuteSqlRaw("InsertGLToGL_FinTrak_Service @crACCountNo,@drACCountNo, @brCode,@CoyCode,@MISCode,@Amount,@TransactionType,@NarrationCr,NarrationDr,@PostedBy,@ApplicationID,@ValueDate,@BatchRef,@TransSequence,@Status", _CrAccount, _DrAccount, _BrCode, _CoyCode, _MISCode, _Amount, _TransactionType, _CrNarration, _DrNarration, _PostedBy, _AppID, _ValueDate, _BatchRef, _transSequence, _Status);

                result = true;
            }
            return result;
        }
        public int InsertFund(string AccountDr, string AccountCr, decimal Amount, int OperationType, int TransactionType, int OperationID, string logUser, string NarrationDr, string NarrationCr, string PostingTime, string Reference, string TransCode, string BrCode, string CoyCode, bool InEntryState, DateTime PostDate, DateTime ValueDate, bool IsCheque, int Appsource, string ChequeNo, bool ChargeStamp, int ChargeType)
        {
            var FundPosting = new InsertFund();
            string transSequence = string.Empty;
            var result = false;
            //transSequence = new Random().Next().ToString().Substring(0, 7);

            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _AccountDr = new SqlParameter("@AccountDr", AccountDr);

                SqlParameter _AccountCr = new SqlParameter("@AccountCr", AccountCr);
                SqlParameter _BrCode = new SqlParameter("@brCode", BrCode);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", GetCompanyCode());
                SqlParameter _OperationType = new SqlParameter("@OperationType", OperationType);
                SqlParameter _Amount = new SqlParameter("@Amount", Amount);
                SqlParameter _TransactionType = new SqlParameter("@TransactionType", TransactionType);
                SqlParameter _CrNarration = new SqlParameter("@NarrationCr", NarrationCr);
                SqlParameter _DrNarration = new SqlParameter("@NarrationDr", NarrationDr);
                SqlParameter _CreateBy = new SqlParameter("@CreateBy", logUser);
                SqlParameter _OperationID = new SqlParameter("@OperationID", OperationID);
                SqlParameter _ValueDate = new SqlParameter("@ValueDate", ValueDate);
                SqlParameter _PostingTime = new SqlParameter("@PostingTime", PostingTime);
                SqlParameter _Reference = new SqlParameter("@Reference", Reference);
                SqlParameter _TransCode = new SqlParameter("@TransCode", TransCode);

                SqlParameter _InEntryState = new SqlParameter("@InEntryState", InEntryState);
                SqlParameter _PostDate = new SqlParameter("@PostDate", PostDate);
                SqlParameter _IsCheque = new SqlParameter("@IsCheque", IsCheque);
                SqlParameter _ChequeNo = new SqlParameter("@ChequeNo", transSequence);
                SqlParameter _ChargeStamp = new SqlParameter("@ChargeStamp", ChargeStamp);
                SqlParameter _ChargeType = new SqlParameter("@ChargeType", ChargeType);
                SqlParameter _Appsource = new SqlParameter("@Appsource", Appsource);
                return context.Database.ExecuteSqlRaw("InsertFundTransfer @AccountDr,@AccountCr, @Amount,@OperationType,@TransactionType,@OperationID,@CreateBy,@NarrationDr,@NarrationCr,@PostingTime,@Reference,@TransCode,@BrCode,@CoyCode,@InEntryState,@PostDate,@ValueDate,@IsCheque,@Appsource,@ChequeNo,@ChargeStamp,@ChargeType", _AccountDr, _AccountCr, _OperationType, _TransactionType, _Amount, _OperationID, _CreateBy, _DrNarration, _CrNarration, _PostingTime, _Reference, _TransCode, _BrCode, _CoyCode, _InEntryState, _PostDate, _ValueDate, _IsCheque, _Appsource, _ChequeNo, _ChargeStamp, _ChargeType);

            }
            //return result;
        }
        public int InsertMultipleFund(string Reference, string AccountNo, string BrCode, string CoyCode, decimal Amount, int OperationID, string NarrationCr, string TransCode, DateTime ValueDate, DateTime PostDate, string logUser, bool InEntryState, int TransactionType, int OperationType, string BatchName, int Appsource,bool IsJournal,bool IsReciept,bool Approved, bool Disapproved)
        {

           using (var context = new TheCoreBankingContext())
            {
                SqlParameter _Reference = new SqlParameter("@Reference", Reference);
                SqlParameter _AccountNo = new SqlParameter("@AccountNo", AccountNo);
                SqlParameter _BrCode = new SqlParameter("@BrCode", BrCode);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", CoyCode);
                SqlParameter _Amount = new SqlParameter("@Amount", Amount);
                SqlParameter _OperationID = new SqlParameter("@OperationID", OperationID);
                SqlParameter _CrNarration = new SqlParameter("@Narration", NarrationCr);
                SqlParameter _TransCode = new SqlParameter("@TransCode", TransCode);
                SqlParameter _ValueDate = new SqlParameter("@ValueDate", ValueDate);
                SqlParameter _PostDate = new SqlParameter("@DateCreated", PostDate);
                SqlParameter _CreateBy = new SqlParameter("@CreatedBy", logUser);
                SqlParameter _InEntryState = new SqlParameter("@InEntryState", @InEntryState);
                SqlParameter _TransactionType = new SqlParameter("@TransactionType", TransactionType);
                SqlParameter _OperationType = new SqlParameter("@OperationType", OperationType);             
                
                SqlParameter _BatchName = new SqlParameter("@BatchName", BatchName);
                SqlParameter _Appsource = new SqlParameter("@AppSource", Appsource);
                SqlParameter _IsJournal = new SqlParameter("@IsJournal", IsJournal);
                SqlParameter _IsReciept = new SqlParameter("@IsReciept", IsReciept);

                SqlParameter _Approved = new SqlParameter("@Approved", Approved);
                SqlParameter _Disapproved = new SqlParameter("@Disapproved", Disapproved);
                
                
                return context.Database.ExecuteSqlRaw("InsertCreditMultiple_FundTransfer @Reference,@AccountNo, @BrCode,@CoyCode,@Amount,@OperationID,@Narration,@TransCode,@ValueDate,@DateCreated,@CreatedBy,@InEntryState,@TransactionType,@OperationType,@BatchName,@AppSource,@IsJournal,@IsReciept,@Approved,@Disapproved", _Reference, _AccountNo, _BrCode, _CoyCode, _Amount, _OperationID, _CrNarration, _TransCode, _ValueDate, _PostDate, _CreateBy, _InEntryState, _TransactionType, _OperationType, _BatchName, _Appsource, _IsJournal, _IsReciept, _Approved, _Disapproved);

            }
            //return result;
        }
        public int InsertTransfer(string CustCode, string ProductAcctNo, string AccountName, string BranchId, string CoyCode, string MISCode, DateTime DateCreated, string TransAcctName, string TransAcctNo, string TransBank, string TransBankAddr, int TransAcctType, decimal AmtTransfered, string Remark, bool Approved, int OperationID, bool Disapproved, decimal Balance, bool Internal, decimal TransAcctBal, string TransAcctCustCode, bool SourceCA, bool DestinationCA, string TransTime, string SlipNumber, string Narration, bool IsReversed, bool SameCustomer, decimal Charge, DateTime ActualDate, string Narration2)
        {
            var FundPosting = new InsertTransfer();
            string transSequence = string.Empty;
            var result = false;
            //transSequence = new Random().Next().ToString().Substring(0, 7);

            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _CustCode = new SqlParameter("@CustCode", CustCode);

                SqlParameter _ProductAcctNo = new SqlParameter("@ProductAcctNo", ProductAcctNo);
                SqlParameter _AccountName = new SqlParameter("@AccountName", AccountName);
                SqlParameter _BranchId = new SqlParameter("@BranchId", BranchId);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", GetCompanyCode());
                SqlParameter _MISCode = new SqlParameter("@MISCode", MISCode);
                SqlParameter _DateCreated = new SqlParameter("@DateCreated", DateCreated);
                SqlParameter _TransAcctName = new SqlParameter("@TransAcctName", TransAcctName);
                SqlParameter _TransAcctNo = new SqlParameter("@TransAcctNo", TransAcctNo);
                SqlParameter _TransBank = new SqlParameter("@TransBank", TransBank);
                SqlParameter _TransBankAddr = new SqlParameter("@TransBankAddr", TransBankAddr);
                SqlParameter _TransAcctType = new SqlParameter("@TransAcctType", TransAcctType);
                SqlParameter _AmtTransfered = new SqlParameter("@AmtTransfered", AmtTransfered);
                SqlParameter _Remark = new SqlParameter("@Remark", Remark);
                SqlParameter _Approved = new SqlParameter("@Approved", Approved);
                SqlParameter _OperationID = new SqlParameter("@OperationID", OperationID);
                SqlParameter _Disapproved = new SqlParameter("@Disapproved", Disapproved);
                SqlParameter _Balance = new SqlParameter("@Balance", Balance);
                SqlParameter _Internal = new SqlParameter("@Internal", Internal);
                SqlParameter _TransAcctBal = new SqlParameter("@TransAcctBal", TransAcctBal);
                SqlParameter _TransAcctCustCode = new SqlParameter("@TransAcctCustCode", TransAcctCustCode);
                SqlParameter _SourceCA = new SqlParameter("@SourceCA", SourceCA);
                SqlParameter _DestinationCA = new SqlParameter("@DestinationCA", DestinationCA);

                SqlParameter _TransTime = new SqlParameter("@TransTime", TransTime);
                SqlParameter _SlipNumber = new SqlParameter("@SlipNumber", SlipNumber);
                SqlParameter _Narration = new SqlParameter("@Narration", Narration);
                SqlParameter _IsReversed = new SqlParameter("@IsReversed", IsReversed);
                SqlParameter _SameCustomer = new SqlParameter("@SameCustomer", SameCustomer);
                SqlParameter _Charge = new SqlParameter("@Charge", Charge);
                SqlParameter _ActualDate = new SqlParameter("@ActualDate", ActualDate);
                SqlParameter _Narration2 = new SqlParameter("@Narration2", Narration2);
                return context.Database.ExecuteSqlRaw("InsertBulkTransfer @CustCode,@ProductAcctNo, @AccountName,@BranchId,@CoyCode,@MISCode,@DateCreated,@TransAcctName,@TransAcctNo,@TransBank,@TransBankAddr,@TransAcctType,@AmtTransfered,@Remark,@Approved,@OperationID,@Disapproved,@Balance,@Internal,@TransAcctBal,@TransAcctCustCode,@SourceCA,@DestinationCA,@TransTime,@SlipNumber,@Narration,@IsReversed,@SameCustomer,@Charge,@ActualDate,@Narration2", _CustCode, _ProductAcctNo, _AccountName, _BranchId, _CoyCode, _MISCode, _DateCreated, _TransAcctName, _TransAcctNo, _TransBank, _TransBankAddr, _TransAcctType, _AmtTransfered, _Remark, _Approved, _OperationID, _Disapproved, _Balance, _Internal, _TransAcctBal, _TransAcctCustCode, _SourceCA, _DestinationCA, _TransTime, _SlipNumber, _Narration, _IsReversed, _SameCustomer, _Charge, _ActualDate, _Narration2);

            }
            //return result;
        }
        private string GetCompanyCode()
        {
            try
            {
                string result;
                TblCompanyInformation company = new TblCompanyInformation();
                using (TheCoreBankingContext db = new TheCoreBankingContext())
                {
                    company = (from a in db.TblCompanyInformation select a).FirstOrDefault();
                }
                if (company != null)
                {
                    result = company.CoyId;
                }
                else
                { result = "Record not found"; }
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public int InsertAccountType(string Description, int AccountCategoryID, bool Active, int SubCaptionID)
        {

            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _Description = new SqlParameter("@Description", Description);

                SqlParameter _AccountCategoryID = new SqlParameter("@AccountCategoryID", AccountCategoryID);
                SqlParameter _Active = new SqlParameter("@Active", Active);
                SqlParameter _SubCaptionID = new SqlParameter("@SubCaptionID", SubCaptionID);

                return context.Database.ExecuteSqlRaw("InsertAccountType @Description,@AccountCategoryID, @Active,@SubCaptionID", _Description, _AccountCategoryID, _Active, _SubCaptionID);

            }
            //return result;
        }

        public List<GetAccountType> ChartOfAccountNumber(string AccounttypeID)
        {
            List<GetAccountType> getAccountType = new List<GetAccountType>();
            try
            {
                //GetAccountType getAccountType = new GetAccountType();
                using (var context = new TheCoreBankingContext())
                {
                    SqlParameter _AccounttypeID = new SqlParameter("@AccounttypeID", AccounttypeID);

                    return context.GetAccountType.FromSqlRaw("sp_BankingChartOfAccountNumber @AccounttypeID", _AccounttypeID).ToList();
                    // return getAccountType= context.Database.ExecuteSqlRaw("sp_BankingChartOfAccountNumber @AccounttypeID", _AccounttypeID).;

                }
            }
            catch (Exception ex)
            {

                return getAccountType;
            }
            //return result;
        }

        public int InsertBulkUploadTransfer(string BatchRef, string PostedBy, string BrCode, string CoyCode, string postTime, string InEntryState, DateTime PostDate, bool ChargeStamp, int TransferChargeType)
        {

            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _BatchRef = new SqlParameter("@BatchRef", BatchRef);
                SqlParameter _PostedBy = new SqlParameter("@PostedBy", PostedBy);
                SqlParameter _BrCode = new SqlParameter("@BrCode", BrCode);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", CoyCode);
                SqlParameter _postTime = new SqlParameter("@postTime", postTime);
                SqlParameter _InEntryState = new SqlParameter("@InEntryState", InEntryState);
                SqlParameter _PostDate = new SqlParameter("@PostDate", PostDate);
                SqlParameter _ChargeStamp = new SqlParameter("@ChargeStamp", ChargeStamp);
                SqlParameter _TransferChargeType = new SqlParameter("@TransferChargeType", TransferChargeType);
                return context.Database.ExecuteSqlRaw("InsertBulkUploadTransfer @BatchRef,@PostedBy, @BrCode,@CoyCode,@postTime,@InEntryState, @PostDate,@ChargeStamp,@TransferChargeType", _BatchRef, _PostedBy, _BrCode, _CoyCode, _postTime, _InEntryState, _PostDate, _ChargeStamp, _TransferChargeType);
            }
        }

        public int InsertBulkTransferUpload(string BatchRef, string PostedBy, string BrCode, string CoyCode, bool InEntryState, bool ChargeStamp, int TransferChargeType, string AccountNo, decimal Amount, string BatchName, string Narration, DateTime PostDate, int ProcessType)
        {

            using (var context = new TheCoreBankingContext())
            {
                SqlParameter _BatchRef = new SqlParameter("@BatchRef", BatchRef);
                SqlParameter _PostedBy = new SqlParameter("@PostedBy", PostedBy);
                SqlParameter _BrCode = new SqlParameter("@BrCode", BrCode);
                SqlParameter _CoyCode = new SqlParameter("@CoyCode", CoyCode);               
                SqlParameter _InEntryState = new SqlParameter("@InEntryState", InEntryState);
                SqlParameter _ChargeStamp = new SqlParameter("@ChargeStamp", ChargeStamp);
                SqlParameter _TransferChargeType = new SqlParameter("@TransferChargeType", TransferChargeType);                
                SqlParameter _AccountNo = new SqlParameter("@AccountNo", AccountNo);
                SqlParameter _Amount = new SqlParameter("@Amount", Amount);
                SqlParameter _BatchName = new SqlParameter("@BatchName", BatchName);
                SqlParameter _Narration = new SqlParameter("@Narration", Narration);
                SqlParameter _PostDate = new SqlParameter("@PostDate", PostDate);
                SqlParameter _ProcessType = new SqlParameter("@ProcessType", ProcessType);               
                return context.Database.ExecuteSqlRaw("InsertBulkTransferUpload @BatchRef,@PostedBy, @BrCode,@CoyCode,@InEntryState,@ChargeStamp,@TransferChargeType,@AccountNo,@Amount,@BatchName,@Narration,@PostDate,@ProcessType", _BatchRef, _PostedBy, _BrCode, _CoyCode, _InEntryState, _ChargeStamp, _TransferChargeType, _AccountNo, _Amount, _BatchName, _Narration, _PostDate, _ProcessType);
            }
        }
    }
}