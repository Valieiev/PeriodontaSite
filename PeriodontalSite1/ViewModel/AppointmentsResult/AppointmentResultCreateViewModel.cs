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

        [Display(Name = "Прием")]
        [Required]
        public int? AppoitmentId { get; set; }

        [Display(Name = "Услуга")]
        [Required]
        public int PriceId { get; set; }

        [Display(Name = "Количество")]
        [Required]
        public int Count { get; set; }

        public List<SelectListItem> Price { get; set; }
    }
}

