using System;
using System.Collections.Generic;
using System.Text;

namespace TheCorebanking.Finance.Data.Models
{
    public class sp_ListGL
    {
        public int Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? PostDate { get; set; }
        public string Ref { get; set; }
        public decimal? DebitAmt { get; set; }
        public decimal? CreditAmt { get; set; }
        public string AccountId { get; set; }
        public string PostedBy { get; set; }
        
       
      
    }
}
