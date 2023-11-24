using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TheCoreBanking.Finance.Data.Models;
using TheCorebanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Data.Contracts
{
    public interface IApprovalTrackRepository : IRepository<TblApprovalTrack>
    {
        IQueryable<TblApprovalTrack> ValidateTrack(int ID);
    }
}
