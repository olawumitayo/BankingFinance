using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    
   public interface ICompanyRepository : IRepository<TblCompanyInformation>
    {
        //IEnumerable<TblCompanyInformation> Company { get;}
        //TblCompanyInformation GetTblCompany(int id);
        IQueryable<TblCompanyInformation> ValidateCompany(string companyId);

    }
}
