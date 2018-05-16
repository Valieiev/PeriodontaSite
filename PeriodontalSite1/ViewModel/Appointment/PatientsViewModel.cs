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
        [Display(Name = "PatientsId")]
        [Required]
        public int PatientsId { get; set; }

        [Display(Name = "FirstName")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "MiddleName")]
        [Required]
        public string MiddleName { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date Birth")]
        public DateTime BirthDate { get; set; }

        public IPagedList<PatientsViewModel> Patients { get; set; }
    }
}