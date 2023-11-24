using System;
using System.Collections.Generic;
using System.Text;

namespace TheCorebanking.Finance.Data.Models
{
    public class sp_MultipleandSingle
    {
        public int Id { get; set; }
        //public string AccountDr { get; set; }
        //public string AccountCr { get; set; }
       
        //public string CreateBy { get; set; }
        //public DateTime PostDate { get; set; }
        public string Reference { get; set; }       
        public bool Approved { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
      
        public string AccountNo { get; set; }
        public string AccountNoDr { get; set; }

       
    }

    public class sp_Single
    {
        public long Id { get; set; }
        public string AccountDr { get; set; }
        public string AccountCr { get; set; }

        public string CreateBy { get; set; }

        public DateTime PostDate { get; set; }
        public string Reference { get; set; }
        public bool Approved { get; set; }
        public decimal Amount { get; set; }
       


    }
}
