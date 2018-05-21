using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Appointment
{
    public class PatientsViewModel
    {

        [Required]
        public int PatientsId { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "{0} должно быть указано")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "{0} должна быть указана")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "{0} должно быть указано")]
        public string MiddleName { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "{0} должен быть указан")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "{0} должна быть указана")]
        public DateTime BirthDate { get; set; }

        public IPagedList<PatientsViewModel> Patients { get; set; }
    }
}