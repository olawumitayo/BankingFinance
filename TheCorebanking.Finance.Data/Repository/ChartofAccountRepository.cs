using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using Microsoft.Data.SqlClient;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class ChartofAccountRepository : EFRepository<TblFinanceChartOfAccount>, IChartofAccountRepository
    {
        public ChartofAccountRepository(TheCoreBankingContext context) : base(context) { }

        public ISetupUnitOfWork setupUnitOfWork { get; set; }

  
        public IQueryable<TblFinanceChartOfAccount> ValidateChart(string ID )
        {
            return dbSet.Where(ps => ps.AccountId ==ID);
        }

        //For Stored Procedure
        public bool branchGenerator(string brLocation)
        {
            var result = false;
            using (var context = new TheCoreBankingContext())
            {
                var companyInfo = setupUnitOfWork.Company.GetAll().FirstOrDefault();

                var branchInfo = setupUnitOfWork.Branch.Get().Where(s => s.CoyId == companyInfo.Id.ToString());

                SqlParameter branch = new SqlParameter("@location", branchInfo);

                context.Database.ExecuteSqlRaw("dbo.sp_Banking_CreateBranch @location", branchInfo);
                result = true;
            }
            return result;
        }
    }
}
