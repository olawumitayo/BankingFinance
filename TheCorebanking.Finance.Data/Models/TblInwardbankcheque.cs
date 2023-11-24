using System;
using System.Collections.Generic;
using System.Text;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblInwardbankcheque
    {
        public int Id { get; set; }
        public string Casaaccountno { get; set; }
        public string Casaaccountname { get; set; }
        public decimal Amount { get; set; }
        public decimal Amountdifference { get; set; }
        public DateTime Transactiondate { get; set; }
        public DateTime Datecreated { get; set; }
        public string Createdby { get; set; }
        public string Companyid { get; set; }
        public string Branchid { get; set; }
        public string Chequeleaveno { get; set; }
        public int Principalglid { get; set; }
        public int? Chargeglid { get; set; }
        public decimal? Chargeamount { get; set; }
        public decimal? Chargepercent { get; set; }
        public bool Approved { get; set; }
        public string Approvedby { get; set; }
        public DateTime? Dateapproved { get; set; }
        public int? Operationid { get; set; }
        public string Narration { get; set; }
        public string Comment { get; set; }
        public bool Isreturned { get; set; }
        public bool Isreversed { get; set; }
        public bool Chargecot { get; set; }
        public decimal? Cotamount { get; set; }
        public bool Otherreturncheque { get; set; }
        public bool Reusecheque { get; set; }
        public bool Isdiscountcharge { get; set; }
        public bool Isreturncharge { get; set; }
        public int? Chequebookdetailid { get; set; }
        public string Receivercasaaccountno { get; set; }
        public string Receivercasaaccountname { get; set; }
        public int? Receiverprincipalglid { get; set; }
    }
}
