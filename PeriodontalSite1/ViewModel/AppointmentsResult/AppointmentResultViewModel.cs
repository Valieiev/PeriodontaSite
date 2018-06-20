using PagedList;
using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel.AppointmentsResult
{
    public class AppointmentResultViewModel
    {

        [Display(Name = "AppointmentResultId")]
        [Required]
        public int AppointmentResultId { get; set; }

        [Display(Name = "Количество")]
        [Required]
        public int Count { get; set; }

        [Display(Name = "Прием")]
        [Required]
        public int AppoitmentId { get; set; }


        [Display(Name = "Цена")]
        [Required]
        public int PriceId { get; set; }

        [Display(Name = "Прием")]
        [Required]
        public Appointments Appoitment { get; set; }
        [Display(Name = "Цена")]
        [Required]
        public Prices Price { get; set; }

        [Display(Name = "Прием")]
        [Required]
        public int CreatedAppoitmentId { get; set; }
        public List<SelectListItem> AppoitmentList { get; set; }
        public IPagedList<AppointmentResultViewModel> AppointmentResults { get; set; }
    }
}