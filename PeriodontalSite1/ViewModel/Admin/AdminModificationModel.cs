using PeriodontalSite1.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PeriodontalSite1.ViewModel.Admin
{
    public class AdminModificationModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        [Required]
        public IList<string> Members { get; set; }
    }
}