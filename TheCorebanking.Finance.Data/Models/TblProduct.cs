using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblProduct
    {
        public long Id { get; set; }
        public string Productid { get; set; }
        public int? Companyid { get; set; }
        public int? Producttypeid { get; set; }
        public int? Productcategoryid { get; set; }
        public bool? Ismultiplecurency { get; set; }
        public short? Productclassid { get; set; }
        public string Productcode { get; set; }
        public string Productname { get; set; }
        public string Productdescription { get; set; }
        public int? Principalbalancegl { get; set; }
        public int? Interestincomeexpensegl { get; set; }
        public int? Interestreceivablepayablegl { get; set; }
        public int? Dormantgl { get; set; }
        public int? Premiumdiscountgl { get; set; }
        public short? Dealtypeid { get; set; }
        public short? Dealclassificationid { get; set; }
        public short? Daycountconventionid { get; set; }
        public short? Scheduletypeid { get; set; }
        public bool? Allowscheduletypeoverride { get; set; }
        public int? Maximumtenor { get; set; }
        public int? Minimumtenor { get; set; }
        public decimal? Maximumrate { get; set; }
        public decimal? Minimumrate { get; set; }
        public decimal? Minimumbalance { get; set; }
        public short? Productpriceindexid { get; set; }
        public double? Productpriceindexspread { get; set; }
        public short? ProductBehaviourid { get; set; }
        public bool? Allowoverdrawn { get; set; }
        public int? Overdrawngl { get; set; }
        public bool? Allowrate { get; set; }
        public bool? Allowtenor { get; set; }
        public bool? Allowmoratorium { get; set; }
        public bool? Allowcustomeraccountforcedebit { get; set; }
        public int? Defaultgraceperiod { get; set; }
        public int? Cleanupperiod { get; set; }
        public int? Expiryperiod { get; set; }
        public double? Equitycontribution { get; set; }
        public int? Maximumdrawdownduration { get; set; }
        public int? Approvedby { get; set; }
        public bool? Completed { get; set; }
        public bool? Approved { get; set; }
        public int? Createdby { get; set; }
        public int? Lastupdatedby { get; set; }
        public DateTime? Datetimecreated { get; set; }
        public DateTime? Datetimeupdated { get; set; }
        public bool? Deleted { get; set; }
        public int? Deletedby { get; set; }
        public DateTime? Datetimedeleted { get; set; }
    }
}
