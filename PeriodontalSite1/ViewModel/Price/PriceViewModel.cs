using PagedList;
using PeriodontalSite1.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PeriodontalSite1.ViewModel
{
    public class PriceViewModel
    {
        [Display(Name = "PriceId")]
        [Required]
        public int PriceId { get; set; }

        [Display(Name = "Цена")]
        [Required]
        public double Value { get; set; }

        [Display(Name = "С даты")]
        [Required]
        public DateTime FromDate { get; set; }


        [Display(Name = "По дату")]
        [Required]
        public DateTime ToDate { get; set; }
        
        [Display(Name = "Услуга")]
        [Required]
        public Models.Services Services { get; set; }

        [Required]
        [Display(Name = "На дату:")]
        public DateTime? EditFromDate { get; set; }


        [Display(Name = "С какой даты активна цена:")]
        public DateTime? DateTimeFilter { get; set; }
        public IPagedList<ResultEdit> Prices { get; set; }
    }
}
