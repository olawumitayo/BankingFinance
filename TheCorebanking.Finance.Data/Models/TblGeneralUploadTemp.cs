using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblGeneralUploadTemp
    {
        public long Id { get; set; }
        public string Dr { get; set; }
        public string Cr { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? IsCheque { get; set; }
        public string ChequeNo { get; set; }
        public decimal? Amount { get; set; }
        public string NarrationDr { get; set; }
        public string NarrationCr { get; set; }
        public long OperationId { get; set; }
        public long TransType { get; set; }
    }
}
