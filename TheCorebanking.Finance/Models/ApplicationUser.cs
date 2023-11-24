using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public TblStaffInformation tblStaffInformation { get; set; }
    }
}
