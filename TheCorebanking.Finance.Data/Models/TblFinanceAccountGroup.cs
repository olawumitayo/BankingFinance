using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceAccountGroup
    {
        public long Id { get; set; }
        public string Description { get; set; }
       
        public string Active { get; set; }
    }
}
