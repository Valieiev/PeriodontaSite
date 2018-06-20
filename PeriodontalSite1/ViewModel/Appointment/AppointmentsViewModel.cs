using PagedList;
using PeriodontalSite1.Models;
using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PeriodontalSite1.Models.Appointments;

namespace PeriodontalSite1.ViewModel.Appointment
{
    public class AppointmentsViewModel
    {

        [Display(Name = "Дата:")]
        public DateTime? DateStart { get; set; }


        [Display(Name = "AppointmentId")]
        [Required]
        public int AppointmentsId { get; set; }


        [Display(Name = "Дата приема")]
        [Required]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Статус")]
        [Required]
        public Status AppointmentStatus { get; set; }

        [Display(Name = "Доктор")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "Пациент")]
        [Required]
        public int PatientId { get; set; }

        [Display(Name = "Доктора")]
        [Required]
        public ApplicationUser User { get; set; }

        [Display(Name = "Пациенты")]
        [Required]
        public Patients Patient { get; set; }

        public IPagedList<AppointmentsViewModel> Appointments { get; set; }
    }
}