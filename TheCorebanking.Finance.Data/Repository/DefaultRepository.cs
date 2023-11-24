using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class DefaultRepository : EFRepository<vw_Banking_DefaultAccounts>, IDefaultRepository
    {
        public DefaultRepository(TheCoreBankingContext context) : base(context) { }

    }
}
