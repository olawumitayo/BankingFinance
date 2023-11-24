using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblServiceSetup
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
