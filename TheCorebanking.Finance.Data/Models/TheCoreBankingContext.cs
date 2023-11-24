using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TheCorebanking.Finance.Data;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data;
using TheCoreBanking.Finance.Data.Models;
using static TheCoreBanking.Finance.Data.FinanceDataModel;

public partial class TheCoreBankingContext : DbContext
    {

        public TheCoreBankingContext()
        {
        }

        public TheCoreBankingContext(DbContextOptions<TheCoreBankingContext> options)
               : base(options)
        {
        }
    public virtual DbSet<sp_ListGL> sp_ListGL { get; set; }
    public virtual DbSet<Gllevel> Gllevel { get; set; }
    public virtual DbSet<TblBankingFundTransferUpload> TblBankingFundTransferUpload { get; set; }
    public virtual DbSet<TblGlpolicy> TblGlpolicy { get; set; }
    public virtual DbSet<TblChartOfAccount> TblChartOfAccount { get; set; }
    public virtual DbSet<TblBankingPostType> TblBankingPostType { get; set; }
    public virtual DbSet<TblBankingAuditTrail> TblBankingAuditTrail { get; set; }
    public virtual DbSet<ApprovalTrack2> ApprovalTrack2 { get; set; }
    public virtual DbSet<TblBankingTransferUploadTemp> TblBankingTransferUploadTemp { get; set; }
    public virtual DbSet<GetAccountType> GetAccountType { get; set; }
    public virtual DbSet<sp_MultipleandSingle> sp_MultipleandSingle { get; set; }
    public virtual DbSet<TblApprovalTrack> TblApprovalTrack { get; set; }
    public virtual DbSet<sp_ListBulkReversal> sp_ListBulkReversal { get; set; }
    public virtual DbSet<sp_Single> sp_Single { get; set; }
    public virtual DbSet<TblBankingCamultipleTransfer> TblBankingCamultipleTransfer { get; set; }
    public virtual DbSet<TblBankingCottransaction> TblBankingCottransaction { get; set; }
    public virtual DbSet<TblBankingCatransfer> TblBankingCatransfer { get; set; }
    public virtual DbSet<GLBalance> GLBalance { get; set; }
    public virtual DbSet<TblProductGroup> TblProductGroup { get; set; }
    public virtual DbSet<TblBankingCawithdrawal> TblBankingCawithdrawal { get; set; }
    public virtual DbSet<TblTilllimitsetup> TblTilllimitsetup { get; set; }
    public virtual DbSet<GetCustomerBalance> GetCustomerBalance { get; set; }
    public virtual DbSet<TblTransferChargeType> TblTransferChargeType { get; set; }
    public virtual DbSet<TblFinanceTransaction> TblFinanceTransaction { get; set; }
    public virtual DbSet<TblGeneralUploadTemp> TblGeneralUploadTemp { get; set; }
    public virtual DbSet<TblFinanceCurrentDate> TblFinanceCurrentDate { get; set; }
    public virtual DbSet<TblSecurityUsers> TblSecurityUsers { get; set; }
    public virtual DbSet<TblBankingAllBanks> TblBankingAllBanks { get; set; }
    public virtual DbSet<TblBatchOperation> TblBatchOperation { get; set; }
    public virtual DbSet<TblCasa> TblCasa { get; set; }
    public virtual DbSet<TblInwardbankcheque> TblInwardbankcheque { get; set; }
    public virtual DbSet<TblBankingAccountToDebit> TblBankingAccountToDebit { get; set; }
    public virtual DbSet<TblMultipleAccountToCreditFundTransfer> TblMultipleAccountToCreditFundTransfer { get; set; }
    public virtual DbSet<TblMultipleAccountToDebitFundTransfer> TblMultipleAccountToDebitFundTransfer { get; set; }
    public virtual DbSet<TblOperationType> TblOperationType { get; set; }
    public virtual DbSet<TblStaffInformation> TblStaffInformation { get; set; }
    public virtual DbSet<TblBankingReversal> TblBankingReversal { get; set; }
    public virtual DbSet<FinanceBankingDefaultAccounts> VwBankingDefaultAccounts { get; set; }
    public virtual DbSet<TblBankingChequeConfirmation> TblBankingChequeConfirmation { get; set; }
    public virtual DbSet<TblBankingChequeLocationSetup> TblBankingChequeLocationSetup { get; set; }
    public virtual DbSet<TblBankingCustomerPrivilege> TblBankingCustomerPrivilege { get; set; }
    public virtual DbSet<TblCustomer> TblCustomer { get; set; }
    public virtual DbSet<TblBankingBatchTransferUploadTemp> TblBankingBatchTransferUploadTemp { get; set; }
    public virtual DbSet<TblBankingModeofId> TblBankingModeofId { get; set; }
        public virtual DbSet<TblBankingProductChargesSetup> TblBankingProductChargesSetup { get; set; }
        public virtual DbSet<TblBankingSector> TblBankingSector { get; set; }
        public virtual DbSet<TblBankingSensitiveCustomers> TblBankingSensitiveCustomers { get; set; }
    public virtual DbSet<TblFinanceChartOfAccount> TblFinanceChartOfAccount { get; set; }
    public virtual DbSet<TblBankingSensitiveUsers> TblBankingSensitiveUsers { get; set; }
        public virtual DbSet<TblBranchDepartmentUnit> TblBranchDepartmentUnit { get; set; }
        public virtual DbSet<TblBranchInformation> TblBranchInformation { get; set; }
        public virtual DbSet<TblCompanyInformation> TblCompanyInformation { get; set; }
        public virtual DbSet<TblTellersetup> TblTellersetup { get; set; }
        public virtual DbSet<TblMultipleAccountToCreditFundTransferTemp> TblMultipleAccountToCreditFundTransferTemp { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblDesignation> TblDesignation { get; set; }
        public virtual DbSet<TblFinanceAccountCategory> TblFinanceAccountCategory { get; set; }
        public virtual DbSet<TblFinanceAccountGroup> TblFinanceAccountGroup { get; set; }
        public virtual DbSet<TblFinanceAccountSub> TblFinanceAccountSub { get; set; }
        public virtual DbSet<TblFinanceAccountType> TblFinanceAccountType { get; set; }
        public virtual DbSet<TblFinanceBank> TblFinanceBank { get; set; }
        public virtual DbSet<TblFinanceBankReconciliation> TblFinanceBankReconciliation { get; set; }
        public virtual DbSet<TblFinanceBankReconciliationDetails> TblFinanceBankReconciliationDetails { get; set; }
        public virtual DbSet<TblFinanceBankStatementsItems> TblFinanceBankStatementsItems { get; set; }

    public virtual DbSet<TblProduct> TblProduct { get; set; }
    public virtual DbSet<TblFinanceBonuses> TblFinanceBonuses { get; set; }
    public virtual DbSet<TblBankingGeneralUpload> TblBankingGeneralUpload { get; set; }
    public virtual DbSet<TblFinanceBonusesGeneral> TblFinanceBonusesGeneral { get; set; }
        public virtual DbSet<TblSingleFundTransfer> TblSingleFundTransfer { get; set; }
        public virtual DbSet<TblFinanceCostCenter> TblFinanceCostCenter { get; set; }
        public virtual DbSet<TblFinanceCounterpartyTransaction> TblFinanceCounterpartyTransaction { get; set; }
        public virtual DbSet<TblFinanceDefaultAccounts> TblFinanceDefaultAccounts { get; set; }
        public virtual DbSet<TblFinanceGlmapping> TblFinanceGlmapping { get; set; }
        public virtual DbSet<TblFinanceStatus> TblFinanceStatus { get; set; }
        public virtual DbSet<TblMisinformation> TblMisinformation { get; set; }
        public virtual DbSet<TblOperationApproval> TblOperationApproval { get; set; }
        public virtual DbSet<TblOperationApprovalCommittee> TblOperationApprovalCommittee { get; set; }
        public virtual DbSet<TblRank> TblRank { get; set; }
        public virtual DbSet<TblServiceSetup> TblServiceSetup { get; set; }
        public virtual DbSet<TblTitle> TblTitle { get; set; }
        public virtual DbSet<TblUnit> TblUnit { get; set; }
        public virtual DbSet<TblFinanceCurrency> TblFinanceCurrency { get; set; }

    // Unable to generate entity type for table 'dbo.vw_ChartOfAccount'. Please see the warning messages.

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            optionsBuilder.UseSqlServer(@"Server=.\fintraksql;Database=TheCoreBankingAzure;User Id=sa;Password=sqluser10$;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<TblBankingReversal>(entity =>
        {
            entity.ToTable("tbl_BankingReversal", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateReversed).HasColumnType("datetime");

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.Property(e => e.PostedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.ReversedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

            entity.Property(e => e.TrasactionDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblApprovalTrack>(entity =>
        {
            entity.ToTable("tbl_ApprovalTrack", "GeneralSetup");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.ALevel).HasColumnName("aLevel");

            entity.Property(e => e.Brcode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Coycode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.MaxAmount)
                .HasColumnName("maxAmount")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.MinAmount)
                .HasColumnName("minAmount")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.OperationDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.OperationName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Staffid)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("GenerateInterestTransID");

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblBankingCawithdrawal>(entity =>
        {
            entity.ToTable("TBL_Banking_CAWithdrawal", "Retail");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.ActualDate).HasColumnType("datetime");
            entity.Property(e => e.ChargeStamp).HasColumnName("ChargeStamp");

            entity.Property(e => e.Addresss)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.AmtWithdraw).HasColumnType("money");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Balance).HasColumnType("money");

            entity.Property(e => e.BranchId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Chequeno)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CustCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CustomerBr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.DestinationCa).HasColumnName("DestinationCA");

            entity.Property(e => e.FormId)
                .HasColumnName("FormID")
                .HasColumnType("char(2)");

            entity.Property(e => e.Miscode)
                .HasColumnName("MISCode")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.PrincipalBalGl)
                .HasColumnName("PrincipalBalGL")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ProductAcctNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.SlipNumber)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.SourceCa).HasColumnName("SourceCA");

            entity.Property(e => e.TillAcct)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransTime)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.UploadDoc)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblBankingFundTransferUpload>(entity =>
        {
            entity.ToTable("tbl_BankingFundTransferUpload", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.AccountNoDr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BatchName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.TransCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblInwardbankcheque>(entity =>
        {
            entity.ToTable("TBL_INWARDBANKCHEQUE", "Retail");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Amount)
                .HasColumnName("AMOUNT")
                .HasColumnType("decimal(18, 5)");

            entity.Property(e => e.Amountdifference)
                .HasColumnName("AMOUNTDIFFERENCE")
                .HasColumnType("decimal(18, 5)");

            entity.Property(e => e.Approved).HasColumnName("APPROVED");

            entity.Property(e => e.Approvedby)
                .HasColumnName("APPROVEDBY")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Branchid)
                .HasColumnName("BRANCHID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Casaaccountname)
                .IsRequired()
                .HasColumnName("CASAACCOUNTNAME")
                .HasMaxLength(100);

            entity.Property(e => e.Casaaccountno)
                .IsRequired()
                .HasColumnName("CASAACCOUNTNO")
                .HasMaxLength(50);

            entity.Property(e => e.Chargeamount)
                .HasColumnName("CHARGEAMOUNT")
                .HasColumnType("decimal(18, 5)");

            entity.Property(e => e.Chargecot).HasColumnName("CHARGECOT");

            entity.Property(e => e.Chargeglid).HasColumnName("CHARGEGLID");

            entity.Property(e => e.Chargepercent)
                .HasColumnName("CHARGEPERCENT")
                .HasColumnType("decimal(5, 2)");

            entity.Property(e => e.Chequebookdetailid).HasColumnName("CHEQUEBOOKDETAILID");

            entity.Property(e => e.Chequeleaveno)
                .IsRequired()
                .HasColumnName("CHEQUELEAVENO")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Comment)
                .HasColumnName("COMMENT")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Companyid)
                .HasColumnName("COMPANYID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Cotamount)
                .HasColumnName("COTAMOUNT")
                .HasColumnType("decimal(18, 5)");

            entity.Property(e => e.Createdby)
                .HasColumnName("CREATEDBY")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Dateapproved)
                .HasColumnName("DATEAPPROVED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datecreated)
                .HasColumnName("DATECREATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Isdiscountcharge).HasColumnName("ISDISCOUNTCHARGE");

            entity.Property(e => e.Isreturncharge).HasColumnName("ISRETURNCHARGE");

            entity.Property(e => e.Isreturned).HasColumnName("ISRETURNED");

            entity.Property(e => e.Isreversed).HasColumnName("ISREVERSED");

            entity.Property(e => e.Narration)
                .HasColumnName("NARRATION")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Operationid).HasColumnName("OPERATIONID");

            entity.Property(e => e.Otherreturncheque).HasColumnName("OTHERRETURNCHEQUE");

            entity.Property(e => e.Principalglid).HasColumnName("PRINCIPALGLID");

            entity.Property(e => e.Receivercasaaccountname)
                .HasColumnName("RECEIVERCASAACCOUNTNAME")
                .HasMaxLength(50);

            entity.Property(e => e.Receivercasaaccountno)
                .HasColumnName("RECEIVERCASAACCOUNTNO")
                .HasMaxLength(50);

            entity.Property(e => e.Receiverprincipalglid)
                .HasColumnName("RECEIVERPRINCIPALGLID")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Reusecheque).HasColumnName("REUSECHEQUE");

            entity.Property(e => e.Transactiondate)
                .HasColumnName("TRANSACTIONDATE")
                .HasColumnType("datetime");
        });
        modelBuilder.HasSequence("GenerateInterestTransID");

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblGlpolicy>(entity =>
        {
            entity.ToTable("tbl_GLPolicy", "Finance");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CurrencyId)
                .HasColumnName("CurrencyID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.GlLength)
                .HasColumnName("GL_Length")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.PfoductGroupId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblBankingGeneralUpload>(entity =>
        {
            entity.ToTable("tbl_BankingGeneralUpload", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AcctNo1)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AcctNo2)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.Property(e => e.BatchRef)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .IsRequired()
                .HasColumnName("brCode")
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.ChequeNo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Comment).IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .IsRequired()
                .HasColumnName("coyCode")
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.CrNarration)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateReversed).HasColumnType("datetime");

            entity.Property(e => e.DrNarration)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.IsDebit)
                .IsRequired()
                .HasColumnType("char(1)");

            entity.Property(e => e.IsReversedBy)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.Property(e => e.PostedBy)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.Property(e => e.PostingTime)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Ref)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ReversedApprovedBy)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.Property(e => e.TransId).HasColumnName("TransID");

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblGeneralUploadTemp>(entity =>
        {
            entity.ToTable("tbl_GeneralUploadTemp", "Finance");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.ChequeNo)
                .HasColumnName("chequeNo")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Cr)
                .HasColumnName("CR")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Dr)
                .HasColumnName("DR")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.IsCheque).HasColumnName("isCheque");

            entity.Property(e => e.NarrationCr)
                .HasColumnName("NarrationCR")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NarrationDr)
                .HasColumnName("NarrationDR")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("date");
        });
        modelBuilder.Entity<Gllevel>(entity =>
        {
            entity.ToTable("GLLevel", "Finance");

            entity.Property(e => e.LevelName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<GLBalance>(entity =>
        {
            entity.HasKey(e => e.Id);

            //entity.ToTable("tbl_BankingAllBanks", "Credit");

            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.GLBalances)
                .HasMaxLength(100)
                .IsUnicode(false);

          
        });
        modelBuilder.Entity<TblProductGroup>(entity =>
        {
            entity.ToTable("tbl_ProductGroup", "Product");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");

            entity.Property(e => e.Datetimecreated)
                .HasColumnName("DATETIMECREATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimedeleted)
                .HasColumnName("DATETIMEDELETED")
                .HasColumnType("datetime");

            entity.Property(e => e.Deleted).HasColumnName("DELETED");

            entity.Property(e => e.Productgroupcode)
                .IsRequired()
                .HasColumnName("PRODUCTGROUPCODE")
                .HasMaxLength(50);

            entity.Property(e => e.Productgroupid)
                .IsRequired()
                .HasColumnName("PRODUCTGROUPID")
                .HasMaxLength(50);

            entity.Property(e => e.Productgroupname)
                .IsRequired()
                .HasColumnName("PRODUCTGROUPNAME")
                .HasMaxLength(50);
        });

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<GetCustomerBalance>(entity =>
        {
            entity.HasKey(e => e.Id);

            //entity.ToTable("tbl_BankingAllBanks", "Credit");

            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.GetCustomerBalances)
                .HasMaxLength(100)
                .IsUnicode(false);


        });
        modelBuilder.Entity<sp_MultipleandSingle>(entity =>
        {
            entity.HasKey(e => e.Id);

           

            entity.Property(e => e.AccountNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.AccountNoDr)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");           

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);            

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

       
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

          
        });
        modelBuilder.Entity<sp_Single>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.AccountCr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.AccountDr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);


        });
        modelBuilder.Entity<GetAccountType>(entity =>
        {
            entity.HasKey(e => e.id);

            entity.Property(e => e.account)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblMultipleAccountToCreditFundTransferTemp>(entity =>
        {
            entity.ToTable("tbl_MultipleAccountToCredit_FundTransfer_temp", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.AccountNoDr)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BatchName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.ReciepNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.TransCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<ApprovalTrack2>(entity =>
        {
            entity.ToTable("ApprovalTrack2", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.ALevel).HasColumnName("aLevel");

            entity.Property(e => e.Brcode)
                .HasColumnName("brcode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Coycode)
                .HasColumnName("coycode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Maxs).HasColumnName("maxs");

            entity.Property(e => e.Mins).HasColumnName("mins");

            entity.Property(e => e.OpName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Staffid)
                .HasColumnName("staffid")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblBankingPostType>(entity =>
        {
            entity.ToTable("tbl_BankingPostType", "Finance");

            entity.Property(e => e.Posting)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblBankingCottransaction>(entity =>
        {
            entity.ToTable("tbl_BankingCOTTransaction", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Cotamount).HasColumnName("COTAmount");

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CustCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.PdId).HasColumnName("pdID");

            entity.Property(e => e.PostedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ProductAcctNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ReferenceId)
                .HasColumnName("ReferenceID")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblBankingAllBanks>(entity =>
        {
            entity.HasKey(e => e.BankId);

            entity.ToTable("tbl_BankingAllBanks", "Credit");

            entity.Property(e => e.BankId).HasColumnName("BankID");

            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ContactAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.ContactEmail)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ContactPhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Oldname)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblBankingAccountToDebit>(entity =>
        {
            entity.ToTable("tbl_BankingAccountToDebit", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.SlipNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblBankingCatransfer>(entity =>
        {
            entity.ToTable("tbl_BankingCATransfer", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ActualDate).HasColumnType("datetime");

            entity.Property(e => e.AmtTransfered).HasColumnType("money");

            entity.Property(e => e.AppId)
                .HasColumnName("AppID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Balance).HasColumnType("money");

            entity.Property(e => e.BranchId)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CustCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.DestinationCa).HasColumnName("DestinationCA");

            entity.Property(e => e.Miscode)
                .HasColumnName("MISCode")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Narration2).IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.PrincipalBalGl)
                .HasColumnName("PrincipalBalGL")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ProductAcctNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.SlipNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.SourceCa).HasColumnName("SourceCA");

            entity.Property(e => e.TillAcct)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransAcctCustCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransAcctName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransAcctNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransAcctType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransBank)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransBankAddr)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransTime)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblBankingCamultipleTransfer>(entity =>
        {
            entity.ToTable("tbl_BankingCAMultipleTransfer", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.AmtTransfered).HasColumnType("money");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CustCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.FeeCharged).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.ProductAcctNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.SlipNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransTime)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblFinanceChartOfAccount>(entity =>
        {
            entity.ToTable("tbl_FinanceChartOfAccount", "Finance");

            entity.Property(e => e.AccountCategoryId).HasColumnName("AccountCategoryID");

            entity.Property(e => e.AccountGroupId).HasColumnName("AccountGroupID");

            entity.Property(e => e.AccountId)
                .IsRequired()
                .HasColumnName("AccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AccountName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

            entity.Property(e => e.BrId)
                .HasColumnName("brID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyId)
                .IsRequired()
                .HasColumnName("coyID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.FscaptionCode)
                .HasColumnName("FSCaptionCode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.IsParentGl).HasColumnName("isParentGL");

            entity.Property(e => e.LevelId)
                .HasColumnName("LevelID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OldAccountId)
                .HasColumnName("OldAccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OldAccountId1)
                .HasColumnName("OldAccountID1")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.OldAccountId2)
                .HasColumnName("OldAccountID2")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ParentGlid)
                .HasColumnName("ParentGLID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RelationShip)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.StCode).HasColumnName("stCode");

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("GenerateInterestTransID");

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblBankingTransferUploadTemp>(entity =>
        {
            entity.ToTable("tbl_BankingTransferUploadTemp", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountNoCr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.AccountNoDr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BatchRef)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CrName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CrStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DrName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.DrStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NarrationCr)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.NarrationDr)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.Customerid);

            entity.ToTable("TBL_CUSTOMER", "Customer");

            entity.Property(e => e.Customerid).HasColumnName("CUSTOMERID");

            entity.Property(e => e.Accountcreationcomplete).HasColumnName("ACCOUNTCREATIONCOMPLETE");

            entity.Property(e => e.Actedonby)
                .HasColumnName("ACTEDONBY")
                .HasMaxLength(150);

            entity.Property(e => e.Annualincomeid).HasColumnName("ANNUALINCOMEID");

            entity.Property(e => e.Approvalstatus).HasColumnName("APPROVALSTATUS");

            entity.Property(e => e.Bankaccountnumber)
                .HasColumnName("BANKACCOUNTNUMBER")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Bankaccountopeneddate)
                .HasColumnName("BANKACCOUNTOPENEDDATE")
                .HasColumnType("date");

            entity.Property(e => e.Bankaccountypeid).HasColumnName("BANKACCOUNTYPEID");

            entity.Property(e => e.Bankaddress)
                .HasColumnName("BANKADDRESS")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Bankid).HasColumnName("BANKID");

            entity.Property(e => e.Branchid).HasColumnName("BRANCHID");

            entity.Property(e => e.Businesscategoryid).HasColumnName("BUSINESSCATEGORYID");

            entity.Property(e => e.Businessstartdate)
                .HasColumnName("BUSINESSSTARTDATE")
                .HasColumnType("date");

            entity.Property(e => e.Businesswebsite)
                .HasColumnName("BUSINESSWEBSITE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");

            entity.Property(e => e.Creationmailsent).HasColumnName("CREATIONMAILSENT");

            entity.Property(e => e.Creditrating)
                .HasColumnName("CREDITRATING")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Currentemployer)
                .HasColumnName("CURRENTEMPLOYER")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Customeraccounttypeid).HasColumnName("CUSTOMERACCOUNTTYPEID");

            entity.Property(e => e.Customercode)
                .IsRequired()
                .HasColumnName("CUSTOMERCODE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Customersensitivitylevelid).HasColumnName("CUSTOMERSENSITIVITYLEVELID");

            entity.Property(e => e.Dateactedon)
                .HasColumnName("DATEACTEDON")
                .HasColumnType("date");

            entity.Property(e => e.Dateofbirth)
                .HasColumnName("DATEOFBIRTH")
                .HasColumnType("date");

            entity.Property(e => e.Datetimecreated)
                .HasColumnName("DATETIMECREATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimedeleted)
                .HasColumnName("DATETIMEDELETED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimeupdated)
                .HasColumnName("DATETIMEUPDATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Deleted).HasColumnName("DELETED");

            entity.Property(e => e.Deletedby).HasColumnName("DELETEDBY");

            entity.Property(e => e.Educationlevel)
                .HasColumnName("EDUCATIONLEVEL")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Employeddate)
                .HasColumnName("EMPLOYEDDATE")
                .HasColumnType("date");

            entity.Property(e => e.Employmentstatus).HasColumnName("EMPLOYMENTSTATUS");

            entity.Property(e => e.Fax)
                .HasColumnName("FAX")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Firstchilddob)
                .HasColumnName("FIRSTCHILDDOB")
                .HasColumnType("date");

            entity.Property(e => e.Firstchildname)
                .HasColumnName("FIRSTCHILDNAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Firstname)
                .HasColumnName("FIRSTNAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Foreignrpno)
                .HasColumnName("FOREIGNRPNO")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Genderid).HasColumnName("GENDERID");

            entity.Property(e => e.Hometown)
                .HasColumnName("HOMETOWN")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Idexpiry)
                .HasColumnName("IDEXPIRY")
                .HasColumnType("date");

            entity.Property(e => e.Idissueauthority)
                .HasColumnName("IDISSUEAUTHORITY")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Idplaceofissue)
                .HasColumnName("IDPLACEOFISSUE")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Industryid).HasColumnName("INDUSTRYID");

            entity.Property(e => e.Institutiontypeid).HasColumnName("INSTITUTIONTYPEID");

            entity.Property(e => e.Ispoliticallyexposed).HasColumnName("ISPOLITICALLYEXPOSED");

            entity.Property(e => e.Lastupdatedby).HasColumnName("LASTUPDATEDBY");

            entity.Property(e => e.Maritalstatusid).HasColumnName("MARITALSTATUSID");

            entity.Property(e => e.Marriagecertificationdate)
                .HasColumnName("MARRIAGECERTIFICATIONDATE")
                .HasColumnType("date");

            entity.Property(e => e.Modeofidentificationid).HasColumnName("MODEOFIDENTIFICATIONID");

            entity.Property(e => e.Mothersmaidenname)
                .HasColumnName("MOTHERSMAIDENNAME")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Namercparentbody)
                .HasColumnName("NAMERCPARENTBODY")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Namercrelatedcoys)
                .HasColumnName("NAMERCRELATEDCOYS")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Nationality)
                .HasColumnName("NATIONALITY")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Natureofbusiness)
                .HasColumnName("NATUREOFBUSINESS")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Nokaddress)
                .HasColumnName("NOKADDRESS")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Nokdob)
                .HasColumnName("NOKDOB")
                .HasColumnType("date");

            entity.Property(e => e.Nokemail)
                .HasColumnName("NOKEMAIL")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Nokgenderid).HasColumnName("NOKGENDERID");

            entity.Property(e => e.Nokothernames)
                .HasColumnName("NOKOTHERNAMES")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Nokphone)
                .HasColumnName("NOKPHONE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Nokrelationship)
                .HasColumnName("NOKRELATIONSHIP")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Noksurname)
                .HasColumnName("NOKSURNAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Occupation)
                .HasColumnName("OCCUPATION")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Othernames)
                .HasColumnName("OTHERNAMES")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Placeofbirth)
                .HasColumnName("PLACEOFBIRTH")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Pobox)
                .HasColumnName("POBOX")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Previousemployer)
                .HasColumnName("PREVIOUSEMPLOYER")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Rcnumber)
                .HasColumnName("RCNUMBER")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Regionid).HasColumnName("REGIONID");

            entity.Property(e => e.Relationshipofficerid).HasColumnName("RELATIONSHIPOFFICERID");

            entity.Property(e => e.Scumlnumber)
                .HasColumnName("SCUMLNUMBER")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Sectorid).HasColumnName("SECTORID");

            entity.Property(e => e.Sourceoffundid).HasColumnName("SOURCEOFFUNDID");

            entity.Property(e => e.Spousemail)
                .HasColumnName("SPOUSEMAIL")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Spousenamework)
                .HasColumnName("SPOUSENAMEWORK")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Spousephone)
                .HasColumnName("SPOUSEPHONE")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Staffnumber)
                .HasColumnName("STAFFNUMBER")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Stateoforiginid).HasColumnName("STATEOFORIGINID");

            entity.Property(e => e.Stateoriginlgaid).HasColumnName("STATEORIGINLGAID");

            entity.Property(e => e.Surname)
                .HasColumnName("SURNAME")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Taxidnumber)
                .HasColumnName("TAXIDNUMBER")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Titleid).HasColumnName("TITLEID");

            entity.Property(e => e.Weddingdate)
                .HasColumnName("WEDDINGDATE")
                .HasColumnType("date");

            entity.Property(e => e.Workaddress)
                .HasColumnName("WORKADDRESS")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Workcountry).HasColumnName("WORKCOUNTRY");

            entity.Property(e => e.Workphone)
                .HasColumnName("WORKPHONE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Workstate).HasColumnName("WORKSTATE");
        });

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblSecurityUsers>(entity =>
        {
            entity.HasKey(e => e.StaffNumber);

            entity.ToTable("tbl_SecurityUsers", "GeneralSetup");

            entity.Property(e => e.StaffNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .ValueGeneratedNever();

            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Casareports).HasColumnName("CASAReports");

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.LastPasswordChange).HasColumnType("datetime");

            entity.Property(e => e.Miscode)
                .HasColumnName("MISCode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.MultiBranch).HasColumnName("multiBranch");

            entity.Property(e => e.MultiCompany).HasColumnName("multiCompany");

            entity.Property(e => e.NextPasswordChange).HasColumnType("datetime");

            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.StaffName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.StaffNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TmpPassword).HasMaxLength(255);

            entity.Property(e => e.TmpPasswordAnswer).HasMaxLength(255);

            entity.Property(e => e.TmpPasswordQuestion).HasMaxLength(255);
        });
        modelBuilder.Entity<TblBatchOperation>(entity =>
        {
            entity.ToTable("tbl_BatchOperation", "Operation");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.OperationName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblTilllimitsetup>(entity =>
        {
            entity.HasKey(e => e.Tillid);

            entity.ToTable("TBL_TILLLIMITSETUP", "Retail");

            entity.Property(e => e.Tillid).HasColumnName("TILLID");

            entity.Property(e => e.Branchid).HasColumnName("BRANCHID");

            entity.Property(e => e.Companyid).HasColumnName("COMPANYID");

            entity.Property(e => e.Createdby)
                .IsRequired()
                .HasColumnName("CREATEDBY")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Datetimecreated)
                .HasColumnName("DATETIMECREATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimedeleted)
                .HasColumnName("DATETIMEDELETED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimeupdated)
                .HasColumnName("DATETIMEUPDATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Deleted).HasColumnName("DELETED");

            entity.Property(e => e.Deletedby)
                .HasColumnName("DELETEDBY")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Lastupdatedby)
                .HasColumnName("LASTUPDATEDBY")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Limitamount)
                .HasColumnName("LIMITAMOUNT")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Telleraccountid)
                .IsRequired()
                .HasColumnName("TELLERACCOUNTID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Tilluserid).HasColumnName("TILLUSERID");
        });
        modelBuilder.Entity<TblCasa>(entity =>
        {
            //entity.HasKey(e => e.Casaaccountid);

            entity.ToTable("TBL_CASA", "Customer");

            entity.HasIndex(e => e.Accountnumber)
                .HasName("IX_TBL_CASA")
                .IsUnique();

            //entity.Property(e => e.Casaaccountid).HasColumnName("CASAACCOUNTID");

            entity.Property(e => e.Accountname)
                .IsRequired()
                .HasColumnName("ACCOUNTNAME")
                .HasMaxLength(100)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Accountnumber)
                .IsRequired()
                .HasColumnName("ACCOUNTNUMBER")
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Accountofficerdeptid).HasColumnName("ACCOUNTOFFICERDEPTID");

            entity.Property(e => e.Accountofficerid)
                .HasColumnName("ACCOUNTOFFICERID")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Accountstatusid)
                .HasColumnName("ACCOUNTSTATUSID")
                .HasDefaultValueSql("((2))");

            entity.Property(e => e.Actionby)
                .HasColumnName("ACTIONBY")
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Actiondate)
                .HasColumnName("ACTIONDATE")
                .HasColumnType("datetime");

            entity.Property(e => e.Approvalstatusid).HasColumnName("APPROVALSTATUSID");

            entity.Property(e => e.Availablebalance)
                .HasColumnName("AVAILABLEBALANCE")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Branchid).HasColumnName("BRANCHID");

            entity.Property(e => e.Companyid).HasColumnName("COMPANYID");

            entity.Property(e => e.Createdby)
                .HasColumnName("CREATEDBY")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Currencyid)
                .HasColumnName("CURRENCYID")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Customerid).HasColumnName("CUSTOMERID");

            entity.Property(e => e.Datetimecreated)
                .HasColumnName("DATETIMECREATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimedeleted)
                .HasColumnName("DATETIMEDELETED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimeupdated)
                .HasColumnName("DATETIMEUPDATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Deleted).HasColumnName("DELETED");

            entity.Property(e => e.Deletedby)
                .HasColumnName("DELETEDBY")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Effectivedate)
                .HasColumnName("EFFECTIVEDATE")
                .HasColumnType("date");

            entity.Property(e => e.Hasoverdraft).HasColumnName("HASOVERDRAFT");

            entity.Property(e => e.Interestrate)
                .HasColumnName("INTERESTRATE")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Iscurrentaccount).HasColumnName("ISCURRENTACCOUNT");

            entity.Property(e => e.Lastupdatecomment)
                .HasColumnName("LASTUPDATECOMMENT")
                .IsUnicode(false);

            entity.Property(e => e.Lastupdatedby)
                .HasColumnName("LASTUPDATEDBY")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Ledgerbalance)
                .HasColumnName("LEDGERBALANCE")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Mandate)
                .HasColumnName("MANDATE")
                .IsUnicode(false);

            entity.Property(e => e.Miscode)
                .HasColumnName("MISCODE")
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Oldproductaccountnumber1)
                .HasColumnName("OLDPRODUCTACCOUNTNUMBER1")
                .HasMaxLength(50);

            entity.Property(e => e.Oldproductaccountnumber2)
                .HasColumnName("OLDPRODUCTACCOUNTNUMBER2")
                .HasMaxLength(50);

            entity.Property(e => e.Oldproductaccountnumber3)
                .HasColumnName("OLDPRODUCTACCOUNTNUMBER3")
                .HasMaxLength(50);

            entity.Property(e => e.Operationid).HasColumnName("OPERATIONID");

            entity.Property(e => e.Overdraftamount)
                .HasColumnName("OVERDRAFTAMOUNT")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Overdraftexpirydate)
                .HasColumnName("OVERDRAFTEXPIRYDATE")
                .HasColumnType("date");

            entity.Property(e => e.Overdraftinterestrate)
                .HasColumnName("OVERDRAFTINTERESTRATE")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Postnostatusid).HasColumnName("POSTNOSTATUSID");

            entity.Property(e => e.Productid).HasColumnName("PRODUCTID");

            entity.Property(e => e.Relationshipmanagerid)
                .HasColumnName("RELATIONSHIPMANAGERID")
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Relationshipofficerdeptid).HasColumnName("RELATIONSHIPOFFICERDEPTID");

            entity.Property(e => e.Relationshipofficerid)
                .HasColumnName("RELATIONSHIPOFFICERID")
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Teammiscode)
                .HasColumnName("TEAMMISCODE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Tenor)
                .HasColumnName("TENOR")
                .HasDefaultValueSql("((0))");

            entity.Property(e => e.Terminaldate)
                .HasColumnName("TERMINALDATE")
                .HasColumnType("date");
        });
        modelBuilder.Entity<TblTransferChargeType>(entity =>
        {
            entity.ToTable("tbl_TransferChargeType", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.ChargeType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblTellersetup>(entity =>
        {
            entity.ToTable("TBL_TELLERSETUP", "Retail");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Branchid).HasColumnName("BRANCHID");

            entity.Property(e => e.Companyid).HasColumnName("COMPANYID");

            entity.Property(e => e.Createdby)
                .HasColumnName("CREATEDBY")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Datecreated)
                .HasColumnName("DATECREATED")
                .HasColumnType("date");

            entity.Property(e => e.Isdelete).HasColumnName("ISDELETE");

            entity.Property(e => e.Staffinformationid).HasColumnName("STAFFINFORMATIONID");

            entity.Property(e => e.Tellertillaccount)
                .HasColumnName("TELLERTILLACCOUNT")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Tillaccountnumber)
                .IsRequired()
                .HasColumnName("TILLACCOUNTNUMBER")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Tilllimitamount).HasColumnName("TILLLIMITAMOUNT");

            entity.Property(e => e.Tilllimitid).HasColumnName("TILLLIMITID");

            entity.Property(e => e.Tillmappingid).HasColumnName("TILLMAPPINGID");

            entity.Property(e => e.Tillname)
                .IsRequired()
                .HasColumnName("TILLNAME")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Tilluser)
                .IsRequired()
                .HasColumnName("TILLUSER")
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.ToTable("tbl_Product", "Product");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Allowcustomeraccountforcedebit).HasColumnName("ALLOWCUSTOMERACCOUNTFORCEDEBIT");

            entity.Property(e => e.Allowmoratorium).HasColumnName("ALLOWMORATORIUM");

            entity.Property(e => e.Allowoverdrawn).HasColumnName("ALLOWOVERDRAWN");

            entity.Property(e => e.Allowrate).HasColumnName("ALLOWRATE");

            entity.Property(e => e.Allowscheduletypeoverride).HasColumnName("ALLOWSCHEDULETYPEOVERRIDE");

            entity.Property(e => e.Allowtenor).HasColumnName("ALLOWTENOR");

            entity.Property(e => e.Approved).HasColumnName("APPROVED");

            entity.Property(e => e.Approvedby).HasColumnName("APPROVEDBY");

            entity.Property(e => e.Cleanupperiod).HasColumnName("CLEANUPPERIOD");

            entity.Property(e => e.Companyid).HasColumnName("COMPANYID");

            entity.Property(e => e.Completed).HasColumnName("COMPLETED");

            entity.Property(e => e.Createdby).HasColumnName("CREATEDBY");

            entity.Property(e => e.Datetimecreated)
                .HasColumnName("DATETIMECREATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimedeleted)
                .HasColumnName("DATETIMEDELETED")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetimeupdated)
                .HasColumnName("DATETIMEUPDATED")
                .HasColumnType("datetime");

            entity.Property(e => e.Daycountconventionid).HasColumnName("DAYCOUNTCONVENTIONID");

            entity.Property(e => e.Dealclassificationid).HasColumnName("DEALCLASSIFICATIONID");

            entity.Property(e => e.Dealtypeid).HasColumnName("DEALTYPEID");

            entity.Property(e => e.Defaultgraceperiod).HasColumnName("DEFAULTGRACEPERIOD");

            entity.Property(e => e.Deleted).HasColumnName("DELETED");

            entity.Property(e => e.Deletedby).HasColumnName("DELETEDBY");

            entity.Property(e => e.Dormantgl).HasColumnName("DORMANTGL");

            entity.Property(e => e.Equitycontribution).HasColumnName("EQUITYCONTRIBUTION");

            entity.Property(e => e.Expiryperiod).HasColumnName("EXPIRYPERIOD");

            entity.Property(e => e.Interestincomeexpensegl).HasColumnName("INTERESTINCOMEEXPENSEGL");

            entity.Property(e => e.Interestreceivablepayablegl).HasColumnName("INTERESTRECEIVABLEPAYABLEGL");

            entity.Property(e => e.Ismultiplecurency).HasColumnName("ISMULTIPLECURENCY");

            entity.Property(e => e.Lastupdatedby).HasColumnName("LASTUPDATEDBY");

            entity.Property(e => e.Maximumdrawdownduration).HasColumnName("MAXIMUMDRAWDOWNDURATION");

            entity.Property(e => e.Maximumrate).HasColumnName("MAXIMUMRATE");

            entity.Property(e => e.Maximumtenor).HasColumnName("MAXIMUMTENOR");

            entity.Property(e => e.Minimumbalance).HasColumnName("MINIMUMBALANCE");

            entity.Property(e => e.Minimumrate).HasColumnName("MINIMUMRATE");

            entity.Property(e => e.Minimumtenor).HasColumnName("MINIMUMTENOR");

            entity.Property(e => e.Overdrawngl).HasColumnName("OVERDRAWNGL");

            entity.Property(e => e.Premiumdiscountgl).HasColumnName("PREMIUMDISCOUNTGL");

            entity.Property(e => e.Principalbalancegl).HasColumnName("PRINCIPALBALANCEGL");

            entity.Property(e => e.ProductBehaviourid).HasColumnName("PRODUCT_BEHAVIOURID");

            entity.Property(e => e.Productcategoryid).HasColumnName("PRODUCTCATEGORYID");

            entity.Property(e => e.Productclassid).HasColumnName("PRODUCTCLASSID");

            entity.Property(e => e.Productcode)
                .HasColumnName("PRODUCTCODE")
                .HasMaxLength(50);

            entity.Property(e => e.Productdescription)
                .HasColumnName("PRODUCTDESCRIPTION")
                .HasMaxLength(200);

            entity.Property(e => e.Productid)
                .HasColumnName("PRODUCTID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Productname)
                .HasColumnName("PRODUCTNAME")
                .HasMaxLength(200);

            entity.Property(e => e.Productpriceindexid).HasColumnName("PRODUCTPRICEINDEXID");

            entity.Property(e => e.Productpriceindexspread).HasColumnName("PRODUCTPRICEINDEXSPREAD");

            entity.Property(e => e.Producttypeid).HasColumnName("PRODUCTTYPEID");

            entity.Property(e => e.Scheduletypeid).HasColumnName("SCHEDULETYPEID");
        });
        modelBuilder.Entity<TblMultipleAccountToCreditFundTransfer>(entity =>
        {
            entity.ToTable("tbl_MultipleAccountToCredit_FundTransfer", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AccountNoDr)
            .HasMaxLength(100)
            .IsUnicode(false);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BatchName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.ReciepNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.TransCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblBankingAuditTrail>(entity =>
        {
            entity.ToTable("tbl_BankingAuditTrail", "GeneralSetup");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.BrCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CmpName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DeptCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Ipaddress)
                .HasColumnName("IPAddress")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransDate).HasColumnType("datetime");

            entity.Property(e => e.TransTime)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TransType).IsUnicode(false);

            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("GenerateInterestTransID");

        modelBuilder.HasSequence("seqGetNextBatchRef")
            .StartsAt(25000)
            .HasMin(25000);

        modelBuilder.HasSequence("TransactionSequence").HasMin(0);
        modelBuilder.Entity<TblMultipleAccountToDebitFundTransfer>(entity =>
        {
            entity.ToTable("tbl_MultipleAccountToDebit_FundTransfer", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.Property(e => e.Narration)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblOperationType>(entity =>
        {
            entity.ToTable("tbl_OperationType", "Operation");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.OperationType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblBankingBatchTransferUploadTemp>(entity =>
        {
            entity.ToTable("tbl_BankingBatchTransferUploadTemp", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountName)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.AccountNo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BatchRef)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Narration)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.HasSequence("TransactionSequence").HasMin(0); modelBuilder.Entity<TblBankingChequeConfirmation>(entity =>
            {
                entity.ToTable("tbl_BankingChequeConfirmation", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BeneficiaryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmedFrom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateConfirmed).HasColumnType("datetime");

                entity.Property(e => e.IsApproved).HasColumnName("Is_Approved");

                entity.Property(e => e.PnoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        modelBuilder.Entity<TblSingleFundTransfer>(entity =>
        {
            entity.ToTable("tbl_SingleFundTransfer", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountCr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.AccountDr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BrCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreateBy)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.NarrationCr)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.NarrationDr)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.Property(e => e.PostingTime)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.ReciepNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.TransCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblFinanceCurrency>(entity =>
        {
            entity.HasKey(e => e.CurrCode);

            entity.ToTable("tbl_FinanceCurrency", "Finance");

            entity.Property(e => e.CountryCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CurrName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CurrSymbol)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.Property(e => e.ExchangeRate).HasColumnType("decimal(10, 4)");
        });
        modelBuilder.Entity<TblFinanceCurrentDate>(entity =>
        {
            entity.ToTable("tbl_FinanceCurrentDate", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.CurrentDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblFinanceTransaction>(entity =>
        {
            entity.ToTable("tbl_FinanceTransaction", "Finance");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountId)
                .HasColumnName("AccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.BatchRef)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreditAmt).HasColumnType("money");

            entity.Property(e => e.DebitAmt).HasColumnType("money");

            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.DestinationBranch)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Itemid)
                .HasColumnName("ITEMID")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.LcurrencyCode)
                .HasColumnName("LCurrencyCode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Legtype)
                .HasColumnName("LEGTYPE")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Miscode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NonBrAccountId)
                .HasColumnName("NonBrAccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.PostedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.PostingTime)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Ref)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.SCoyCode)
                .HasColumnName("sCoyCode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Sbu)
                .HasColumnName("SBU")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SourceBranch)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.Property(e => e.ValueDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblStaffInformation>(entity =>
        {
            entity.ToTable("tbl_StaffInformation", "GeneralSetup");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Age).HasColumnType("datetime");

            entity.Property(e => e.BranchId)
                .HasColumnName("BranchID")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CompanyId)
                .HasColumnName("CompanyID")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.DeptCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Gender)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Miscode)
                .HasColumnName("MISCode")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.NextOfKinAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.NextOfKinEmail)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.NextOfKinGender)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NextOfKinName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.NextOfKinPhone)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PcCode)
                .HasColumnName("pcCode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Rank)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.RelationShip)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.StaffName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.StaffNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Staffsignature).HasColumnType("image");

            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Unit)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.UnitCode)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblBankingChequeLocationSetup>(entity =>
            {
                entity.ToTable("tbl_BankingChequeLocationSetup", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBankingCustomerPrivilege>(entity =>
            {
                entity.ToTable("tbl_BankingCustomerPrivilege", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("smalldatetime");

                entity.Property(e => e.DateLastUpdated).HasColumnType("smalldatetime");

                entity.Property(e => e.GetUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBankingModeofId>(entity =>
            {
                entity.ToTable("tbl_BankingModeofID", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Coy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idmode)
                    .HasColumnName("IDMode")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBankingProductChargesSetup>(entity =>
            {
                entity.ToTable("tbl_BankingProductChargesSetup", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Op)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Operation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PdTypeId).HasColumnName("PdTypeID");

                entity.Property(e => e.ProdType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Targets)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBankingSector>(entity =>
            {
                entity.ToTable("tbl_BankingSector", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Sector)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBankingSensitiveCustomers>(entity =>
            {
                entity.ToTable("tbl_BankingSensitiveCustomers", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Approvedby)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("smalldatetime");

                entity.Property(e => e.Dateccreated).HasColumnType("smalldatetime");

                entity.Property(e => e.OperationId).HasColumnName("OperationID");
            });
        modelBuilder.Entity<FinanceBankingDefaultAccounts>(entity =>
        {
            entity.ToTable("FinanceBankingDefaultAccounts");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.AccountId)
                .IsRequired()
                .HasColumnName("AccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AccountName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TblBankingSensitiveUsers>(entity =>
            {
                entity.ToTable("tbl_BankingSensitiveUsers", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Approvedby)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateApproved).HasColumnType("smalldatetime");

                entity.Property(e => e.Dateccreated).HasColumnType("smalldatetime");

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.StaffNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBranchDepartmentUnit>(entity =>
            {
                entity.ToTable("tbl_BranchDepartmentUnit", "GeneralSetup");

                entity.Property(e => e.BranchId)
                    .HasColumnName("BranchID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyId)
                    .HasColumnName("CoyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblBranchInformation>(entity =>
            {
                entity.ToTable("tbl_BranchInformation", "GeneralSetup");

                entity.Property(e => e.BrAddress)
                    .HasColumnName("brAddress")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrId)
                    .HasColumnName("brID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrLocation)
                    .HasColumnName("brLocation")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BrManager)
                    .HasColumnName("brManager")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BrName)
                    .HasColumnName("brName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrState)
                    .HasColumnName("brState")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CoyId)
                    .HasColumnName("coyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCompanyInformation>(entity =>
            {
                entity.ToTable("tbl_CompanyInformation", "GeneralSetup");

                entity.Property(e => e.AccountStand)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.AuthorisedShareCapital).HasColumnType("money");

                entity.Property(e => e.CompanyClass)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyClass)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyId)
                    .HasColumnName("coyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyName)
                    .HasColumnName("coyName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoyRegisteredBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfCommencement).HasColumnType("datetime");

                entity.Property(e => e.DateOfIncorporation).HasColumnType("datetime");

                entity.Property(e => e.DateOfRenewalOfRegistration).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EoyprofitAndLossGl)
                    .HasColumnName("EOYProfitAndLossGL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FormerManagersTrustees)
                    .HasColumnName("FormerManagers_Trustees")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionsRegistered)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvestmentObjective).IsUnicode(false);

                entity.Property(e => e.ManagementType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Manager)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfRegistrar)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NameOfScheme).IsUnicode(false);

                entity.Property(e => e.NameOfTrustees)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NatureOfBusiness)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrusteesAddress).IsUnicode(false);

                entity.Property(e => e.Webbsite)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           
            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.ToTable("tbl_Department", "GeneralSetup");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CoyId)
                    .HasColumnName("CoyID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<TblDepartment>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Com_tbl_Department");
            });

            modelBuilder.Entity<TblDesignation>(entity =>
            {
                entity.ToTable("tbl_Designation", "GeneralSetup");

                entity.Property(e => e.Designation)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceAccountCategory>(entity =>
            {
                entity.ToTable("tbl_FinanceAccountCategory", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountGroupId).HasColumnName("AccountGroupID");

                entity.Property(e => e.Descriptions)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceAccountGroup>(entity =>
            {
                entity.ToTable("tbl_FinanceAccountGroup", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceAccountSub>(entity =>
            {
                entity.ToTable("tbl_FinanceAccountSub", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubId)
                    .HasColumnName("AccountSubID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MsreplTranVersion).HasColumnName("msrepl_tran_version");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceAccountType>(entity =>
            {
                entity.ToTable("tbl_FinanceAccountType", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountCategoryId).HasColumnName("AccountCategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MainCaptionCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubCaptionId).HasColumnName("SubCaptionID");
            });

            modelBuilder.Entity<TblFinanceBank>(entity =>
            {
                entity.ToTable("tbl_FinanceBank", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccNo)
                    .HasColumnName("Acc_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankId)
                    .HasColumnName("BankID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceBankReconciliation>(entity =>
            {
                entity.ToTable("tbl_FinanceBankReconciliation", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankAccNos)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankGlaccNos)
                    .IsRequired()
                    .HasColumnName("BankGLAccNos")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankReconId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Brcode)
                    .HasColumnName("brcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClosingBnkStatementBal).HasColumnType("money");

                entity.Property(e => e.Coycode)
                    .HasColumnName("coycode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntryBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Glbalance)
                    .HasColumnName("GLBalance")
                    .HasColumnType("money");

                entity.Property(e => e.OpeningBnkStatementBal).HasColumnType("money");

                entity.Property(e => e.ReconciliationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFinanceBankReconciliationDetails>(entity =>
            {
                entity.ToTable("tbl_FinanceBankReconciliationDetails", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankReconId)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Brcode)
                    .HasColumnName("brcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Coycode)
                    .HasColumnName("coycode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("money");

                entity.Property(e => e.Debit).HasColumnType("money");

                entity.Property(e => e.Details).IsUnicode(false);

                entity.Property(e => e.EntryBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MatchedNos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReconcilationDate).HasColumnType("datetime");

                entity.Property(e => e.Ref)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceBankStatementsItems>(entity =>
            {
                entity.ToTable("tbl_FinanceBankStatementsItems", "Finance");

                entity.Property(e => e.Approvedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankId)
                    .HasColumnName("BankID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankReconId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Brcode)
                    .HasColumnName("brcode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Coycode)
                    .HasColumnName("coycode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Credit).HasColumnType("money");

                entity.Property(e => e.CreditAccountId)
                    .HasColumnName("CreditAccountID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Debit).HasColumnType("money");

                entity.Property(e => e.DebitAccountId)
                    .HasColumnName("DebitAccountID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntryBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Postedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReconciliationDate).HasColumnType("datetime");

                entity.Property(e => e.Reference).IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceBonuses>(entity =>
            {
                entity.HasKey(e => e.BonusId);

                entity.ToTable("tbl_FinanceBonuses", "Finance");

                entity.Property(e => e.BonusId).HasColumnName("BonusID");

                entity.Property(e => e.ClosureDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ForEvery).HasColumnType("money");

                entity.Property(e => e.MadeBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Security)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Unit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceBonusesGeneral>(entity =>
            {
                entity.HasKey(e => e.BonusId);

                entity.ToTable("tbl_FinanceBonusesGeneral", "Finance");

                entity.Property(e => e.BonusId).HasColumnName("BonusID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClosureDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.MadeBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Security).IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Unit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           

            modelBuilder.Entity<TblFinanceCostCenter>(entity =>
            {
                entity.ToTable("tbl_FinanceCostCenter", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Costname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceCounterpartyTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("tbl_FinanceCounterpartyTransaction", "Finance");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("ApplicationID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BatchRef)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Branch)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CVolume).HasColumnName("cVolume");

                entity.Property(e => e.Coy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CpId)
                    .HasColumnName("cpID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreditAmount).HasColumnType("money");

                entity.Property(e => e.CustCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DVolume).HasColumnName("dVolume");

                entity.Property(e => e.DebitAmount).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.FormNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GlaccountId)
                    .HasColumnName("GLAccountID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Legtype)
                    .HasColumnName("legtype")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OldAccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Show).HasColumnName("show");

                entity.Property(e => e.SystemDateTime).HasColumnType("datetime");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionType).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        modelBuilder.Entity<sp_ListBulkReversal>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

           

            entity.Property(e => e.BatchRef)
                .HasMaxLength(100)
                .IsUnicode(false);

          

          

            entity.Property(e => e.CreditAmount).HasColumnType("money");

           

            entity.Property(e => e.DebitAmount).HasColumnType("money");

           
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.Property(e => e.Ref)
                .HasMaxLength(50)
                .IsUnicode(false);

           
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

           

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblFinanceDefaultAccounts>(entity =>
        {
            entity.HasKey(e => e.DfId);

            entity.ToTable("tbl_FinanceDefaultAccounts", "Finance");

            entity.Property(e => e.DfId).HasColumnName("dfID");

            entity.Property(e => e.AccountId)
                .HasColumnName("AccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AccountName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateApproved).HasColumnType("smalldatetime");

            entity.Property(e => e.DateCreated).HasColumnType("smalldatetime");

            entity.Property(e => e.DfDescription)
                .HasColumnName("dfDescription")
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.FinancePnc)
                .HasColumnName("FinancePNC")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.FinancePnd)
                .HasColumnName("FinancePND")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.LastUpdate).HasColumnType("smalldatetime");

            entity.Property(e => e.TellerPnc)
                .HasColumnName("TellerPNC")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.TellerPnd)
                .HasColumnName("TellerPND")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblFinanceGlmapping>(entity =>
            {
                entity.ToTable("tbl_FinanceGLMapping", "Finance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FinTrakGl)
                    .HasColumnName("FinTrakGL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FinTrakGlcode)
                    .HasColumnName("FinTrakGLCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OtherGl)
                    .HasColumnName("OtherGL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OtherGlcode)
                    .HasColumnName("OtherGLCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFinanceStatus>(entity =>
            {
                entity.HasKey(e => e.StCode);

                entity.ToTable("tbl_FinanceStatus", "Finance");

                entity.Property(e => e.StCode).HasColumnName("stCode");

                entity.Property(e => e.StStatus)
                    .IsRequired()
                    .HasColumnName("stStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMisinformation>(entity =>
            {
                entity.ToTable("tbl_MISInformation", "GeneralSetup");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MisCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MisName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ParentMisCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

        modelBuilder.Entity<TblOperationApproval>(entity =>
        {
            entity.ToTable("tbl_OperationApproval", "GeneralSetup");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Approvedby)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ApprovingAuthority).IsUnicode(false);

            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Comment).IsUnicode(false);

            entity.Property(e => e.CommitteeType)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CummulativeAmt).HasColumnType("money");

            entity.Property(e => e.DateApproved).HasColumnType("datetime");

            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.MaxAmt).HasColumnType("money");

            entity.Property(e => e.MinAmt).HasColumnType("money");

            entity.Property(e => e.Miscode)
                .HasColumnName("MISCode")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.OpId).HasColumnName("OpID");

            entity.Property(e => e.OperationId).HasColumnName("OperationID");

            entity.Property(e => e.Ref)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.StaffId)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

      
       

        modelBuilder.Entity<TblOperationApprovalCommittee>(entity =>
            {
                entity.ToTable("tbl_OperationApprovalCommittee", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovingAuthority).IsUnicode(false);

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CommitteeType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CummulativeAmt).HasColumnType("money");

                entity.Property(e => e.DesignationN)
                    .HasColumnName("Designation_N")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaxAmt).HasColumnType("money");

                entity.Property(e => e.MinAmt).HasColumnType("money");

                entity.Property(e => e.Miscode)
                    .HasColumnName("MISCode")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRank>(entity =>
            {
                entity.ToTable("tbl_Rank", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Rank)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblServiceSetup>(entity =>
            {
                entity.ToTable("tbl_ServiceSetup", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblTitle>(entity =>
            {
                entity.ToTable("tbl_Title", "GeneralSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

       

        modelBuilder.Entity<TblFinanceChartOfAccount>(entity =>
        {
            entity.ToTable("tbl_FinanceChartOfAccount", "Finance");
            
            entity.Property(e => e.AccountCategoryId).HasColumnName("AccountCategoryID");

            entity.Property(e => e.AccountGroupId).HasColumnName("AccountGroupID");

            entity.Property(e => e.AccountId)
                .IsRequired()
                .HasColumnName("AccountID")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.AccountName)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

            entity.Property(e => e.BrId)
                .HasColumnName("brID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CoyId)
                .HasColumnName("coyID")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
         

            entity.Property(e => e.StCode).HasColumnName("stCode");

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BrSpecific)
             .HasMaxLength(50)
             .IsUnicode(false);
        });
        modelBuilder.Entity<TblUnit>(entity =>
            {
                entity.ToTable("tbl_Unit", "GeneralSetup");

                entity.Property(e => e.BrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
   //view in context class
        modelBuilder.Entity<vw_CustomerAndGLAccount>(entity =>
        {
            entity.ToTable("vw_CustomerAndGLAccount", "dbo");
        });
        modelBuilder.Entity<vw_Banking_DefaultAccounts>(entity =>
        {
            entity.ToTable("vw_Banking_DefaultAccounts", "dbo");
        });
    }
    }

