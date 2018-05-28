using PagedList;
using System;
using System.ComponentModel.DataAnnotations;

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
        public Models.Services Services { get; set; }

        public bool filtrEnable { get; set; }
        public DateTime DateTimeFilter { get; set; }
        public IPagedList<PriceViewModel> Prices { get; set; }
    }
}
