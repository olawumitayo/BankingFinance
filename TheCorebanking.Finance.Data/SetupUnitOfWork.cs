using Microsoft.EntityFrameworkCore;
using System;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data;
using TheCoreBanking.Finance.Data.Contracts;



namespace TheCoreBanking.Finance.Data
{
    /// <summary>
    /// The Code Camper "Unit of Work"
    ///     1) decouples the repos from the controllers
    ///     2) decouples the DbContext and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type such as a <see cref="Person"/>.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data layer (which is the EF DbContext in Code Camper).
    /// </remarks>
    public class SetupUnitOfWork : ISetupUnitOfWork, IDisposable
    {
        private TheCoreBankingContext DbContext = new TheCoreBankingContext();
        public SetupUnitOfWork(IRepositoryProvider repositoryProvider)
        {
            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }
        protected IRepositoryProvider RepositoryProvider { get; set; }

        // Define repositories
        public IApprovalTrackRepository ApprovalTrack { get { return GetEntityRepository<IApprovalTrackRepository>(); } }
        public IPostTypeRepository PostType { get { return GetEntityRepository<IPostTypeRepository>(); } }
        public ICounterpartyRepository counterparty { get { return GetEntityRepository<ICounterpartyRepository>(); } }
        public IBatchUploadRepository BatchUpload { get { return GetEntityRepository<IBatchUploadRepository>(); } }
        public IAuditRepository Audit { get { return GetEntityRepository<IAuditRepository>(); } }
        public ITillLimitRepository TillLimit { get { return GetEntityRepository<ITillLimitRepository>(); } }
        public IGeneralUploadRepository GeneralUpload { get { return GetEntityRepository<IGeneralUploadRepository>(); } }
        public IGeneralUploadTempRepository GeneralUploadTemp { get { return GetEntityRepository<IGeneralUploadTempRepository>(); } }
        public IDefaultRepository vwDefault { get { return GetEntityRepository<IDefaultRepository>(); } }
        public IReversalRepository Reversal { get { return GetEntityRepository<IReversalRepository>(); } }
        public IMultipleFundTempRepository multipleFundTemp { get { return GetEntityRepository<IMultipleFundTempRepository>(); } }
        public IMaintenanceFeeRepository maintenanceFee { get { return GetEntityRepository<IMaintenanceFeeRepository>(); } }
        public IChargeTypeRepository ChargeType { get { return GetEntityRepository<IChargeTypeRepository>(); } }
        public ITransactionRepository Transaction { get { return GetEntityRepository<ITransactionRepository>(); } }
        public IMultipleFundRepository MultipleFund { get { return GetEntityRepository<IMultipleFundRepository>(); } }
        public ISecurityRepository Security { get { return GetEntityRepository<ISecurityRepository>(); } }
        public ISingleFundRepository SingleFund { get { return GetEntityRepository<ISingleFundRepository>(); } }
        public IChequeConfirmationRepository Cheque { get { return GetEntityRepository<IChequeConfirmationRepository>(); } }
        public IModelofIdRepository Mode { get { return GetEntityRepository<IModelofIdRepository>(); } }
        public ISectorRepository Sector { get { return GetEntityRepository<ISectorRepository>(); } }
        public IServiceSetupRepository Service { get { return GetEntityRepository<IServiceSetupRepository>(); } }
        public IAccountGroupRepository Group { get { return GetEntityRepository<IAccountGroupRepository>(); } }
        public IAccountCategoryRepository Category { get { return GetEntityRepository<IAccountCategoryRepository>(); } }
        public ICostRepository Cost { get { return GetEntityRepository<ICostRepository>(); } }
        public ICurrencyRepository Currency { get { return GetEntityRepository<ICurrencyRepository>(); } }
        public IChartofAccountRepository Chart { get { return GetEntityRepository<IChartofAccountRepository>(); } }

        public ICompanyRepository Company { get { return GetEntityRepository<ICompanyRepository>(); } }
        public IBranchRepository Branch { get { return GetEntityRepository<IBranchRepository>(); } }
        public IAccountTypeRepository AccountType { get { return GetEntityRepository<IAccountTypeRepository>(); } }
        public IStatusRepository Status { get { return GetEntityRepository<IStatusRepository>(); } }
        public IDefaultAccountRepository Default { get { return GetEntityRepository<IDefaultAccountRepository>(); } }
        public ISubRepository Sub { get { return GetEntityRepository<ISubRepository>(); } }
        public IVwDefaultRepository Vw { get { return GetEntityRepository<IVwDefaultRepository>(); } }
        public IGlMappingRepository GL { get { return GetEntityRepository<IGlMappingRepository>(); } }
        public IOperationTypeRepository OperationType { get { return GetEntityRepository<IOperationTypeRepository>(); } }
        public IStaffRepository Staff { get { return GetEntityRepository<IStaffRepository>(); } }
        public ICasaRepository Accounts => GetEntityRepository<ICasaRepository>();
        public IProductRepository Product => GetEntityRepository<IProductRepository>();
        public ITransactionTypeRepository Batch => GetEntityRepository<ITransactionTypeRepository>();
        public IAllBanksRepository Banks => GetEntityRepository<IAllBanksRepository>();
        public ICustomerandGLRepository CustomerAndGL => GetEntityRepository<ICustomerandGLRepository>();
        /// <summary>
        /// 
        /// Save pending changes to the database
        /// </summary>
        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");
            DbContext.SaveChanges();
        }




        private IRepository<T> GetStandardRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }
        private T GetEntityRepository<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }


        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}