using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBankingProductChargesSetup
    {
        public long Id { get; set; }
        public string ProdType { get; set; }
        public int? PdTypeId { get; set; }
        public string Operation { get; set; }
        public string CoyCode { get; set; }
        public string BranchCode { get; set; }
        public string Targets { get; set; }
        public bool? Active { get; set; }
        public string Product { get; set; }
        public string Op { get; set; }
    }
}
