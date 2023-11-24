using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblOperationType
    {
        public int Id { get; set; }
        public string OperationType { get; set; }
        public string BrCode { get; set; }
        public string CoyCode { get; set; }
        public int? Class { get; set; }
    }
}
