using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Models;

namespace TheCoreBanking.Finance.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TblStaffInformation> TblStaffInformation { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\FINTRAKSQL;initial catalog=TheCoreBanking;user id=sa;password=sqluser10$;MultipleActiveResultSets=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<TblStaffInformation>(entity =>
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

        }
    }
}
