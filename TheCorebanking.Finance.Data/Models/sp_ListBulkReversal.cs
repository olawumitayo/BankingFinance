using System;
using System.Collections.Generic;
using System.Text;

namespace TheCorebanking.Finance.Data.Models
{
    public class sp_ListBulkReversal
    {
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Ref { get; set; }
       
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public string UserName { get; set; }
        

        public string BatchRef { get; set; }
        public DateTime? PostDate { get; set; }
  
       
    }
}
