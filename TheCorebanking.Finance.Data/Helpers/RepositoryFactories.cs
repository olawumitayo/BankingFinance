using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Repository;

namespace TheCoreBanking.Finance.Data
{
    /// <summary>
    /// A maker of Code Camper Repositories.
    /// </summary>
    /// <remarks>
    /// An instance of this class contains repository factory functions for different types.
    /// Each factory function takes an EF <see cref="DbContext"/> and returns
    /// a repository bound to that DbContext.
    /// <para>
    /// Designed to be a "Singleton", configured at web application start with
    /// all of the factory functions needed to create any type of repository.
    /// Should be thread-safe to use because it is configured at app start,
    /// before any request for a factory, and should be immutable thereafter.
    /// </para>
    /// </remarks>
    public class RepositoryFactories
    {
        /// <summary>
        /// Return the runtime Code Camper repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM CODE CAMPER FACTORY FUNCTIONS
        /// </remarks>
        private IDictionary<Type, Func<TheCoreBankingContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<TheCoreBankingContext, object>>
                {
                  {typeof(IMultipleFundTempRepository), dbContext => new MultipleFundTempRepository(dbContext)},
                       {typeof(IBatchUploadRepository), dbContext => new BatchUploadRepository(dbContext)},
                   {typeof(IReversalRepository), dbContext => new ReversalRepository(dbContext)},
                    {typeof(ITillLimitRepository), dbContext => new TillLimitRepository(dbContext)},
                 {typeof(IMaintenanceFeeRepository), dbContext => new MaintenanceFeeRepository(dbContext)},
                 {typeof(ITransactionRepository), dbContext => new TransactionRepository(dbContext)},
                   {typeof(ICasaRepository), dbContext => new CasaRepository(dbContext)},
                 {typeof(IAccountGroupRepository), dbContext => new AccountGroupRepository(dbContext)},
                 {typeof(IAccountCategoryRepository), dbContext => new AccountCategoryRepository(dbContext)},
                 {typeof(ICostRepository), dbContext => new CostRepository(dbContext)},
                 {typeof(ICurrencyRepository), dbContext => new CurrencyRepository(dbContext)},
                 {typeof(IChartofAccountRepository), dbContext => new ChartofAccountRepository(dbContext)},
                 {typeof(ICompanyRepository), dbContext => new CompanyRepository(dbContext)},
                {typeof(IBranchRepository), dbContext => new BranchRepository(dbContext)},
                 {typeof(IAccountTypeRepository), dbContext => new AccountTypeRepository(dbContext)},
                    {typeof(IStatusRepository), dbContext => new StatusRepository(dbContext)},
                           {typeof(IDefaultAccountRepository), dbContext => new DefaultAccountRepository(dbContext)},
                 {typeof(ISubRepository), dbContext => new SubRepository(dbContext)},
                    {typeof(IGlMappingRepository), dbContext => new GlMappingRepository(dbContext)},
                          {typeof(IVwDefaultRepository), dbContext => new VwDefaultRepository(dbContext)},
                           {typeof(IOperationTypeRepository), dbContext => new OperationTypeRepository(dbContext)},
                            {typeof(IStaffRepository), dbContext => new StaffRepository(dbContext)},
                                  {typeof(IProductRepository), dbContext => new ProductRepository(dbContext)},
                                   {typeof(ITransactionTypeRepository), dbContext => new TransactionTypeRepository(dbContext)},
                                    {typeof(IAllBanksRepository), dbContext => new AllBanksRepository(dbContext)},
                                      {typeof(ISingleFundRepository), dbContext => new SingleFundRepository(dbContext)},
                {typeof(ISecurityRepository), dbContext => new SecurityRepository(dbContext)},
               {typeof(IMultipleFundRepository), dbContext => new MultipleFundRepository(dbContext)},
                {typeof(IChargeTypeRepository), dbContext => new ChargeTypeRepository(dbContext)},
                {typeof(ICustomerandGLRepository), dbContext => new CustomerandGLRepository(dbContext)},
               {typeof(IDefaultRepository), dbContext => new DefaultRepository(dbContext)},
                {typeof(IGeneralUploadRepository), dbContext => new GeneralUploadRepository(dbContext)},
                  {typeof(IGeneralUploadTempRepository), dbContext => new GeneralUploadTempRepository(dbContext)},
                   {typeof(IAuditRepository), dbContext => new AuditRepository(dbContext)},
                   {typeof(IPostTypeRepository), dbContext => new PostTypeRepository(dbContext)},
                    {typeof(ICounterpartyRepository), dbContext => new CounterpartyRepository(dbContext)},

                       {typeof(IApprovalTrackRepository), dbContext => new ApprovalTrackRepository(dbContext)},
                };
        }

        /// <summary>
        /// Constructor that initializes with runtime Code Camper repository factories
        /// </summary>
        public RepositoryFactories()  
        {
            _repositoryFactories = GetFactories();
        }

        /// <summary>
        /// Constructor that initializes with an arbitrary collection of factories
        /// </summary>
        /// <param name="factories">
        /// The repository factory functions for this instance. 
        /// </param>
        /// <remarks>
        /// This ctor is primarily useful for testing this class
        /// </remarks>
        public RepositoryFactories(IDictionary<Type, Func<TheCoreBankingContext, object>> factories )
        {
            _repositoryFactories = factories;
        }

        /// <summary>
        /// Get the repository factory function for the type.
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null.</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type 
        /// but could be any type (e.g., an entity type)
        /// </remarks>
        public Func<TheCoreBankingContext, object> GetRepositoryFactory<T>()
        {
       
            Func<TheCoreBankingContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="T">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EF <see cref="DbContext"/>.
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="_repositoryFactories"/>.
        /// If not, falls back to the <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// You can substitute an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="_repositoryFactories"/>.
        /// </remarks>
        public Func<TheCoreBankingContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T">Type of the repository's root entity</typeparam>
        protected virtual Func<TheCoreBankingContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EFRepository<T>(dbContext);
        }

        /// <summary>
        /// Get the dictionary of repository factory functions.
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A value is a repository factory function
        /// that takes a <see cref="DbContext"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        private readonly IDictionary<Type, Func<TheCoreBankingContext, object>> _repositoryFactories;

    }
}
