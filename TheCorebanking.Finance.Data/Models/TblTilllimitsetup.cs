using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblTilllimitsetup
    {
        public int Tillid { get; set; }
        public int Tilluserid { get; set; }
        public decimal Limitamount { get; set; }
        public string Telleraccountid { get; set; }
        public int Branchid { get; set; }
        public int Companyid { get; set; }
        public string Createdby { get; set; }
        public string Lastupdatedby { get; set; }
        public DateTime Datetimecreated { get; set; }
        public DateTime? Datetimeupdated { get; set; }
        public bool Deleted { get; set; }
        public string Deletedby { get; set; }
        public DateTime? Datetimedeleted { get; set; }
    }
}
