using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblCasa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Casaaccountid { get; set; }
        public string Accountnumber { get; set; }
        public string Accountname { get; set; }
        public int Customerid { get; set; }
        public int Productid { get; set; }
        public int? Companyid { get; set; }
        public int? Branchid { get; set; }
        public int Currencyid { get; set; }
        public bool Iscurrentaccount { get; set; }
        public int? Tenor { get; set; }
        public decimal? Interestrate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public DateTime? Terminaldate { get; set; }
        public string Actionby { get; set; }
        public DateTime? Actiondate { get; set; }
        public int? Accountstatusid { get; set; }
        public int? Operationid { get; set; }
        public decimal Availablebalance { get; set; }
        public decimal Ledgerbalance { get; set; }
        public string Accountofficerid { get; set; }
        public int? Accountofficerdeptid { get; set; }
        public string Relationshipofficerid { get; set; }
        public string Relationshipmanagerid { get; set; }
        public int? Relationshipofficerdeptid { get; set; }
        public string Mandate { get; set; }
        public string Miscode { get; set; }
        public string Teammiscode { get; set; }
        public decimal? Overdraftamount { get; set; }
        public decimal? Overdraftinterestrate { get; set; }
        public DateTime? Overdraftexpirydate { get; set; }
        public bool? Hasoverdraft { get; set; }
        public int Postnostatusid { get; set; }
        public string Oldproductaccountnumber1 { get; set; }
        public string Oldproductaccountnumber2 { get; set; }
        public string Oldproductaccountnumber3 { get; set; }
        public string Createdby { get; set; }
        public string Lastupdatedby { get; set; }
        public string Lastupdatecomment { get; set; }
        public DateTime? Datetimecreated { get; set; }
        public DateTime? Datetimeupdated { get; set; }
        public bool? Deleted { get; set; }
        public string Deletedby { get; set; }
        public DateTime? Datetimedeleted { get; set; }
        public int? Approvalstatusid { get; set; }
    }
}
