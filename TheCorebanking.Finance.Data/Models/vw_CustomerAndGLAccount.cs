using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheCorebanking.Finance.Data.Models
{
   public class vw_CustomerAndGLAccount
    {

        public long ID { get; set; }
       
        public string Accountnumber { get; set; }
        public string Accountname { get; set; }       
        public bool Iscurrentaccount { get; set; }
      
        public string AccountName { get; set; }
        public int isGL { get; set; }
        public decimal AVAILABLEBALANCE { get; set; }
        public int? OPERATIONID { get; set; }
    }
    public class vw_Banking_DefaultAccounts
    {
        public long ID { get; set; }

        public string AccountID { get; set; }
        public string AccountName { get; set; }

        public string Type { get; set; }
        public string Category { get; set; }
    }
}
