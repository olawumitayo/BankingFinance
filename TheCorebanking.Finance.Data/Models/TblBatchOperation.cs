using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblBatchOperation
    {
        public int Id { get; set; }
        public string OperationName { get; set; }
        public string CoyCode { get; set; }
        public string BrCode { get; set; }
    }
}
