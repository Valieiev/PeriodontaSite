using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public UserType TypeUser { get; set; }
        public string Address { get; set; }
        public DateTime Birth { get; set; }
        public ApplicationUser()
        {
        }

    }
    public enum UserType
    {
        [Description("Receptionist")]
        Receptionist = 1,
        [Description("Dentist")]
        Dentist =2
    }
}