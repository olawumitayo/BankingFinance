using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBranchInformation
    {
        public long Id { get; set; }
        public string BrId { get; set; }
        public string CoyId { get; set; }
        public string BrAddress { get; set; }
        public string BrLocation { get; set; }
        public string BrState { get; set; }
        public string BrManager { get; set; }
        public string BrName { get; set; }
        public bool Deleted { get; set; }
    }
}
