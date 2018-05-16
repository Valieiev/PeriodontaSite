using PeriodontalSite1.Models.Account;
using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel.Admin
{
    public class AdminEditViewModel
    {
        
        public string Id { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Тип пользователя")]
        public UserType UserType { get; set; }
        [Required]
        [Display(Name = "Роли")]
        public IList<string> Members { get; set; }



        public List<ApplicationRole> Roles { get; set; }
    }
}