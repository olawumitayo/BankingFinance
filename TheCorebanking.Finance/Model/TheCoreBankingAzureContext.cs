using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheCorebanking.Finance.Model
{
    public partial class TheCoreBankingAzureContext : DbContext
    {
        public TheCoreBankingAzureContext()
        {
        }

        public TheCoreBankingAzureContext(DbContextOptions<TheCoreBankingAzureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblApprovalTrack> TblApprovalTrack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=fintraksqlmmbs.database.windows.net;Database=TheCoreBankingAzure; user id=fintrak; password=Password20$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
