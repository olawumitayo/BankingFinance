using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using System.Linq;
using TheCorebanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Repository
{
    public class ApprovalTrackRepository : EFRepository<TblApprovalTrack>, IApprovalTrackRepository
    {
        public ApprovalTrackRepository(TheCoreBankingContext context) : base(context) { }



        public IQueryable<TblApprovalTrack> ValidateTrack(int ID )
        {
            return dbSet.Where(ps => ps.Id ==ID);
        }

    }
}
