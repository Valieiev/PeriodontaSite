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

        [Required]
        public string Id { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} должно быть указано")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} должна быть указана")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} должно быть указано")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "{0} должна быть указана")]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }
        [Required(ErrorMessage = "{0} должны быть указан")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "{0} должны быть указан")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Тип пользователя")]
        [Required(ErrorMessage = "{0} должны быть указан")]
        public UserType UserType { get; set; }
        [Required(ErrorMessage = "{0} должны быть указаны")]
        [Display(Name = "Роли")]
        public IList<string> Members { get; set; }
      
        [Display(Name = "Роли")]
        public List<ApplicationRole> Roles { get; set; }
    }
}