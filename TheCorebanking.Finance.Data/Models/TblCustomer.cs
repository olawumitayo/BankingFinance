using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblCustomer
    {
        public int Customerid { get; set; }
        public string Customercode { get; set; }
        public int? Branchid { get; set; }
        public int? Genderid { get; set; }
        public int? Titleid { get; set; }
        public int? Institutiontypeid { get; set; }
        public int? Modeofidentificationid { get; set; }
        public DateTime? Idexpiry { get; set; }
        public string Idissueauthority { get; set; }
        public string Idplaceofissue { get; set; }
        public int? Stateoriginlgaid { get; set; }
        public int? Maritalstatusid { get; set; }
        public int? Annualincomeid { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public string Placeofbirth { get; set; }
        public string Hometown { get; set; }
        public DateTime? Weddingdate { get; set; }
        public DateTime? Marriagecertificationdate { get; set; }
        public string Mothersmaidenname { get; set; }
        public string Educationlevel { get; set; }
        public string Firstchildname { get; set; }
        public DateTime? Firstchilddob { get; set; }
        public string Nationality { get; set; }
        public int? Stateoforiginid { get; set; }
        public string Spousenamework { get; set; }
        public string Spousephone { get; set; }
        public string Spousemail { get; set; }
        public int? Employmentstatus { get; set; }
        public string Occupation { get; set; }
        public string Currentemployer { get; set; }
        public string Workaddress { get; set; }
        public string Workphone { get; set; }
        public int? Workstate { get; set; }
        public int? Workcountry { get; set; }
        public DateTime? Employeddate { get; set; }
        public string Previousemployer { get; set; }
        public int Customeraccounttypeid { get; set; }
        public int? Relationshipofficerid { get; set; }
        public bool? Ispoliticallyexposed { get; set; }
        public int? Customersensitivitylevelid { get; set; }
        public int? Sectorid { get; set; }
        public int? Industryid { get; set; }
        public string Taxidnumber { get; set; }
        public int? Sourceoffundid { get; set; }
        public string Creditrating { get; set; }
        public string Staffnumber { get; set; }
        public int? Bankid { get; set; }
        public string Bankaddress { get; set; }
        public int? Bankaccountypeid { get; set; }
        public DateTime? Bankaccountopeneddate { get; set; }
        public string Bankaccountnumber { get; set; }
        public string Fax { get; set; }
        public string Pobox { get; set; }
        public string Foreignrpno { get; set; }
        public string Noksurname { get; set; }
        public string Nokothernames { get; set; }
        public DateTime? Nokdob { get; set; }
        public string Nokemail { get; set; }
        public string Nokaddress { get; set; }
        public string Nokphone { get; set; }
        public string Nokrelationship { get; set; }
        public int? Nokgenderid { get; set; }
        public int? Regionid { get; set; }
        public string Rcnumber { get; set; }
        public int? Businesscategoryid { get; set; }
        public DateTime? Businessstartdate { get; set; }
        public string Natureofbusiness { get; set; }
        public string Scumlnumber { get; set; }
        public string Businesswebsite { get; set; }
        public string Namercrelatedcoys { get; set; }
        public string Namercparentbody { get; set; }
        public int? Approvalstatus { get; set; }
        public DateTime? Dateactedon { get; set; }
        public string Actedonby { get; set; }
        public bool? Accountcreationcomplete { get; set; }
        public bool? Creationmailsent { get; set; }
        public int? Createdby { get; set; }
        public int? Lastupdatedby { get; set; }
        public DateTime? Datetimecreated { get; set; }
        public DateTime? Datetimeupdated { get; set; }
        public bool? Deleted { get; set; }
        public int? Deletedby { get; set; }
        public DateTime? Datetimedeleted { get; set; }
    }
}
