using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? PepStatus { get; set; }
    }
}
