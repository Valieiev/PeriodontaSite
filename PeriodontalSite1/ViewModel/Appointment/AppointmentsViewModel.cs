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
        [Display(Name = "AppointmentId")]
        [Required]
        public int AppointmentsId { get; set; }


        [Display(Name = "VisitDate")]
        [Required]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Status")]
        [Required]
        public Status AppointmentStatus { get; set; }

        [Display(Name = "UserId")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "PatientId")]
        [Required]
        public int PatientId { get; set; }

        [Display(Name = "Doctors")]
        [Required]
        public ApplicationUser User { get; set; }

        [Display(Name = "Patient")]
        [Required]
        public Patients Patient { get; set; }

        public IPagedList<AppointmentsViewModel> Appointments { get; set; }
    }
}