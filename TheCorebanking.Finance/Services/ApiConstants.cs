using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Services
{
    public class ApiConstants
    {
        public const string BaseApiUrl = "https://www.amdoren.com/api/currency.php?";
        public const string CurrencyEndPoint = "api_key=sTm3rfQpB8uJiefbvhzj9zyEqURDdQ&from={0}&to={1}&amount={2}";
        public const string BaseApiUrls = "http://bankingplatform:8042/";
        //public const string CurrencyEndpoint = "finances/financesetup/listcurrency";
        //public const string ProductEndpoint = "product/ProductMapping/listproduct";
        public const string UserEndpoint = "Customer/Account/LoadAccounts";
        //public const string CompanyEndpoint = "company/companysetup/listcompany";
        //public const string BranchEndpoint = "company/companysetup/listbranch";
        //public const string LoanBookingEndpoint = "credits/creditsetup/listloanlease";
    }
}
