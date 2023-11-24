using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Models
{
    public class BatchTransferUpload
    {
        
            public int Id { get; set; }
            public string AccountNo { get; set; }
            public bool IsCheque { get; set; }
            public string ChequeNo { get; set; }
            public decimal Amount { get; set; }
            public string Narration { get; set; }
            public decimal AccountBalance { get; set; }
            public string AccountName { get; set; }
            public string Status { get; set; }
            public int ErrorCode { get; set; }
            public string BatchRef { get; set; }
            public bool IsDelete { get; set; }
            public bool StampDuty { get; set; }
            public int type             { get; set; }           
            public string acctNodr      { get; set; }
            public decimal amountdr     { get; set; }
            public string BatchName     { get; set; }
            public string Transactiondate { get; set; }
            public decimal TotalCredit { get; set; }


    }                                  
}
