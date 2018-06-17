using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel
{
    public class PriceCreateViewModel
    {
        [Display(Name = "PriceId")]
        [Required]
        public int PriceId { get; set; }


        [Display(Name = "Цена")]
        [Required]
        public double  Value { get; set; }

        [Display(Name = "С даты")]
        [Required]
        public DateTime FromDate { get; set; }


        [Display(Name = "Услуга")]
        [Required]
        public int ServiceSelected { get; set; }


        public List<SelectListItem> Service { get; set; }

    }
}