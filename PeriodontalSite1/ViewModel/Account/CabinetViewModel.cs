using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Account
{
    public class CabinetViewModel
    {
        [Display(Name = "Имя:")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия:")]
        public string LastName { get; set; }

        [Display(Name = "Отчество:")]
        public string MiddleName { get; set; }

        [Display(Name = "Дата рождения:")]
        public DateTime Birth { get; set; }


        [Display(Name = "Номер:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Пол:")]
        public string Sex { get; set; }

        [Display(Name = "Адрес:")]
        public string Address { get; set; }

    }
}