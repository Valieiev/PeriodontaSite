using PeriodontalSite1.Models;
using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static PeriodontalSite1.Models.Appointments;

namespace PeriodontalSite1.ViewModel.Appointment
{
    public class EditAppointmentViewModel
    {

        [Display(Name = "AppointmentId")]
        [Required]
        public int AppointmentsId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public int Status { get; set; }

        public Status AppointmentStatus { get; set; }


    }
}