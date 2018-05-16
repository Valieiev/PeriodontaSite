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
    public class AppointmentCreateViewModel
    {

        [Display(Name = "Status")]
        [Required]
        public Status AppointmentStatus { get; set; }

        [Display(Name = "UserId")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "PatientId")]
        [Required]
        public int PatientId { get; set; }

        [Display(Name = "VisitDate")]
        [Required]
        public DateTime VisitDate { get; set; }

   
        public ApplicationUser User { get; set; }

        public Patients Patient { get; set; }

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Patients { get; set; }

    }
}