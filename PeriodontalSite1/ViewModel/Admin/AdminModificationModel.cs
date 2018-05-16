using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Admin
{
    public class AdminModificationModel
    {
        [Required]
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        [Required]
        public string[] ActiveRole { get; set; }
     
    }
}