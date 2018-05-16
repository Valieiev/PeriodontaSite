using PeriodontalSite1.Models.Account;
using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Role
{
    public class RoleEditViewModel
    {
        public ApplicationRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }

    }
}