


using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    /// <summary>
    /// Interface for the "Unit of Work"
    /// </summary>
    public interface ISetupUnitOfWork
    {
        // Save pending changes to the data store.
        void Commit();
        IApprovalTrackRepository ApprovalTrack { get; }
        ICounterpartyRepository counterparty { get; }
        IBatchUploadRepository BatchUpload { get; }
        IDefaultRepository vwDefault { get; }
        IPostTypeRepository PostType { get; }
        IAuditRepository Audit { get; }
        IGeneralUploadTempRepository GeneralUploadTemp { get; }
        IGeneralUploadRepository GeneralUpload { get; }
        ICustomerandGLRepository CustomerAndGL { get; }
        IReversalRepository Reversal { get; }
        ITillLimitRepository TillLimit{ get; }
        IMultipleFundTempRepository multipleFundTemp { get; }
        IMaintenanceFeeRepository maintenanceFee { get; }
        IChargeTypeRepository ChargeType { get; }
        ITransactionRepository Transaction { get; }
        IMultipleFundRepository MultipleFund { get; }
        ISecurityRepository Security { get; }
        ISingleFundRepository SingleFund { get; }
        IAllBanksRepository Banks { get; }
        ITransactionTypeRepository Batch { get; }
        ICasaRepository Accounts { get; }
        IProductRepository Product { get; }
        IServiceSetupRepository Service { get; }
        ISectorRepository Sector { get; }
        IModelofIdRepository Mode { get; }
        IChequeConfirmationRepository Cheque { get; }
        ICostRepository Cost { get; }
        IAccountGroupRepository Group { get; }
        ICurrencyRepository Currency { get; }
        IAccountCategoryRepository Category { get; }
        IChartofAccountRepository Chart { get; }
        ICompanyRepository Company { get; }
        IBranchRepository Branch { get; }
        IAccountTypeRepository AccountType { get; }
        IStatusRepository Status { get; }
        ISubRepository Sub { get; }
        IDefaultAccountRepository Default { get; }
        IGlMappingRepository GL { get; }
        IVwDefaultRepository Vw { get; }
        IOperationTypeRepository OperationType { get; }
        IStaffRepository Staff { get; }
    }

    
}