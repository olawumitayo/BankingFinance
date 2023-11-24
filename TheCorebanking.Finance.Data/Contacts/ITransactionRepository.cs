using System.Linq;
using System;
using System.Data.SqlClient;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using TheCorebanking.Finance.Data.Models;
using System.Collections.Generic;
using TheCorebanking.Finance.Data;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface ITransactionRepository : IRepository<TblFinanceTransaction>
    {
        IQueryable<TblFinanceTransaction> ValidateTransaction(int id);

        //For Stored Procedure
        decimal SPGLBalance(string accountNo);
        List<sp_ListGL> spGLList(string TranDate, string initiators);
        List<sp_MultipleandSingle> spMultipleSingle(string TranDate, string initiators);
        List<sp_MultipleandSingle> spMultipleSingleGL(string TranDate, string initiators);
        List<sp_Single> spSingleGL(string TranDate, string initiators);
        List<sp_Single> spSingle(string TranDate, string initiators);
        List<sp_ListBulkReversal> spBulkList(string TranDate, string initiators);
        decimal CustomerBalance(string accountNo);
        int InsertMultipleFund(string Reference, string AccountNo, string BrCode, string CoyCode, decimal Amount, int OperationID, string NarrationCr, string TransCode, DateTime ValueDate, DateTime PostDate, string logUser, bool InEntryState, int TransactionType, int OperationType, string BatchName, int Appsource, bool IsJournal, bool IsReciept, bool Approved, bool Disapproved, bool Charge);
        int InsertFund(string AccountDr, string AccountCr, decimal Amount, int OperationType, int TransactionType, int OperationID, string logUser, string NarrationDr, string NarrationCr, string PostingTime, string Reference, string TransCode, string BrCode, string CoyCode, bool InEntryState, DateTime PostDate, DateTime ValueDate, bool IsCheque, int Appsource, string ChequeNo, bool ChargeStamp, int ChargeType);
        bool PostTransaction(string CrAccount, string DrAccount, string BrCode, string CoyCode, string MISCode, decimal Amount, int TransactionType, string CrNarration, string DrNarration, string PostedBy, string AppID,DateTime ValueDate, string BatchRef, string transSequence, bool Status);
        bool PostTransactions(string CrAccount, string DrAccount, string BrCode, string CoyCode, string MISCode, decimal Amount, int TransactionType, string CrNarration, string DrNarration, string PostedBy, string AppID, DateTime ValueDate, string BatchRef, string transSequence, bool Status);
        int InsertBulkUploadTransfer(string BatchRef, string PostedBy, string BrCode, string CoyCode, string postTime, string InEntryState, DateTime PostDate, bool ChargeStamp, int TransferChargeType);
        int InsertTransfer(string CustCode, string ProductAcctNo, string AccountName, string BranchId, string CoyCode, string MISCode, DateTime DateCreated, string TransAcctName, string TransAcctNo, string TransBank, string TransBankAddr, int TransAcctType, decimal AmtTransfered, string Remark, bool Approved, int OperationID, bool Disapproved, decimal Balance, bool Internal, decimal TransAcctBal, string TransAcctCustCode, bool SourceCA, bool DestinationCA, string TransTime, string SlipNumber, string Narration, bool IsReversed, bool SameCustomer, decimal Charge, DateTime ActualDate, string Narration2);
        int InsertAccountType(string Description, int AccountCategoryID, bool Active, int SubCaptionID);
        int InsertBulkTransferUpload(string BatchRef, string PostedBy, string BrCode, string CoyCode, bool InEntryState, bool ChargeStamp, int TransferChargeType,string AccountNo, decimal Amount, string BatchName, string Narration, DateTime PostDate, int ProcessType);
        List<GetAccountType> ChartOfAccountNumber(string AccounttypeID);

    }
}
