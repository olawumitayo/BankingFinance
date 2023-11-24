using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblGlpolicy
    {
        public int Id { get; set; }
        public string GlLength { get; set; }
        public string CompanyCode { get; set; }
        public string BranchCode { get; set; }
        public string PfoductGroupId { get; set; }
        public string AccountType { get; set; }
        public string CurrencyId { get; set; }
    }
}
