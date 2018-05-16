using PagedList;
using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.AppointmentsResult
{
    public class AppointmentResultViewModel
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

        [Display(Name = "Appoitment")]
        [Required]
        public Appointments Appoitment { get; set; }
        [Display(Name = "Price")]
        [Required]
        public Prices Price { get; set; }

        public IPagedList<AppointmentResultViewModel> AppointmentResults { get; set; }
    }
}