using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Admin
{
    public class AdminViewModel : IdentityUser
    {


        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Номер телефона")]
        public override string PhoneNumber { get => base.PhoneNumber ; set => base.PhoneNumber = value; }

        [Display(Name = "Дата рождения")]
        public DateTime Birth { get; set; }

        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public IPagedList<AdminViewModel> Users { get; set; }

    }
}