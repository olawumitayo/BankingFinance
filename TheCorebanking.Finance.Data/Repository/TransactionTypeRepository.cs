using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class TransactionTypeRepository : EFRepository<TblBatchOperation>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblBatchOperation> ValidateBatch(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
