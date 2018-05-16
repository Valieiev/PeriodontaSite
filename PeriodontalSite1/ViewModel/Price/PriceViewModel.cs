using PagedList;
using PeriodontalSite1.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel
{
    public class PriceViewModel
    {
        [Display(Name = "PriceId")]
        [Required]
        public int PriceId { get; set; }

        [Display(Name = "Value")]
        [Required]
        public double Value { get; set; }

        [Display(Name = "FromDate")]
        [Required]
        public DateTime FromDate { get; set; }


        [Display(Name = "ToDate")]
        [Required]
        public DateTime ToDate { get; set; }

        [Display(Name = "Services")]
        [Required]
        public Services Services { get; set; }


        public IPagedList<PriceViewModel> Prices { get; set; }
    }
}
