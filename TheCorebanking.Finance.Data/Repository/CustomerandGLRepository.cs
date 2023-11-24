using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class CustomerandGLRepository : EFRepository<vw_CustomerAndGLAccount>, ICustomerandGLRepository
    {
        public CustomerandGLRepository(TheCoreBankingContext context) : base(context) { }

    }
}
