using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.AppointmentsResult
{
    public class AppointmentResultStatisticViewModel
    {
    
        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public string Doctor { get; set; }

        public List<AppointmentResult> AppoitmentRes { get; set; }
    }
}