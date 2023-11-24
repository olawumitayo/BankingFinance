using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheCoreBanking.Finance.Data.Models;


namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IChequeConfirmationRepository:IRepository<TblBankingChequeConfirmation>
    {
        IQueryable<TblBankingChequeConfirmation> ValidateCheque(int ID);
    }
}
