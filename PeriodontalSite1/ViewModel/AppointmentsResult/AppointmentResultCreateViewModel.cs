using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel.AppointmentsResult
{
    public class AppointmentResultCreateViewModel
    {
        [Display(Name = "AppointmentResultId")]
        [Required]
        public int AppointmentResultId { get; set; }

        [Display(Name = "Count")]
        [Required]
        public int Count { get; set; }

        [Display(Name = "AppoitmentId")]
        [Required]
        public int AppoitmentId { get; set; }

        [Display(Name = "PriceId")]
        [Required]
        public int PriceId { get; set; }

        public List<SelectListItem> Appoitment { get; set; }
        public List<SelectListItem> Price { get; set; }
    }
}