using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


using System.Linq;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Repository;
using TheCoreBanking.Finance.Data.Contracts;

namespace TheCoreBanking.Finance.Data.Repository
{
    
    public class CompanyRepository : EFRepository<TblCompanyInformation>, ICompanyRepository
    {
        //private readonly TheCoreBankingContext _theCoreBankingContext;
        public CompanyRepository(TheCoreBankingContext context) : base(context) { } 

        public IQueryable<TblCompanyInformation> ValidateCompany(string companyId)
        {
            return dbSet.Where(ps => ps.CoyId == companyId);
        }
    }
}
