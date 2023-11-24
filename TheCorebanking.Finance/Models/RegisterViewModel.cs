using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public TblStaffInformation staffInformation { get; set; }
        public string ReturnUrl { get; set; }
    }

    //public class StaffInformation
    //{
    //    public string CompanyID { get; set; }
    //    public string StaffNo { get; set; }
    //    public string MISCode { get; set; }
    //    public string BranchID { get; set; }
    //    public string StaffName { get; set; }
    //    public string Department { get; set; }
    //    public string JobTitle { get; set; }
    //    public string Rank { get; set; }
    //    public string Phone { get; set; }
    //    public string Email { get; set; }
    //    public string Address { get; set; }
    //    public string Age { get; set; }
    //    public string Gender { get; set; }
    //    public string NextOfKinName { get; set; }
    //    public string NextOfKinPhone { get; set; }
    //    public string NextOfKinEmail { get; set; }
    //    public string NextOfKinAddress { get; set; }
    //    public string NextOfKinGender { get; set; }
    //    public string Comment { get; set; }
    //    public string State { get; set; }
    //    public string Nationality { get; set; }
    //    public string Relationship { get; set; }
    //    public string Updated { get; set; }
    //    public string Staffsignature { get; set; }
    //    public string DeptCode { get; set; }
    //    public string UnitCode { get; set; }
    //    public string Unit { get; set; }
    //    public string pcCode { get; set; }
    //}
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
       
    }

    public class UserRoleViewModel
    {
        [Required]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
    public class PermissionViewModel
    {
        [Required]
        [Display(Name = "Permission")]
        public string Permission { get; set; }

    }

}
