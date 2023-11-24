using System;
using System.Collections.Generic;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblCompanyInformation
    {
        public long Id { get; set; }
        public string CoyId { get; set; }
        public string CoyName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfIncorporation { get; set; }
        public string Manager { get; set; }
        public string NatureOfBusiness { get; set; }
        public string NameOfScheme { get; set; }
        public string FunctionsRegistered { get; set; }
        public decimal? AuthorisedShareCapital { get; set; }
        public string NameOfRegistrar { get; set; }
        public string NameOfTrustees { get; set; }
        public string FormerManagersTrustees { get; set; }
        public DateTime? DateOfRenewalOfRegistration { get; set; }
        public DateTime? DateOfCommencement { get; set; }
        public int? InitialFloatation { get; set; }
        public int? InitialSubscription { get; set; }
        public string CoyRegisteredBy { get; set; }
        public string TrusteesAddress { get; set; }
        public string InvestmentObjective { get; set; }
        public string CompanyClass { get; set; }
        public string CompanyType { get; set; }
        public int? AccountingStandard { get; set; }
        public int? MgtType { get; set; }
        public string Webbsite { get; set; }
        public string CoyClass { get; set; }
        public string AccountStand { get; set; }
        public string ManagementType { get; set; }
        public string EoyprofitAndLossGl { get; set; }
    }
}
