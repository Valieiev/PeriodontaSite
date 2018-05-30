using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Price
{
    public class PriceEditViewModel
    {
        [Display(Name = "На дату:")]
        public DateTime? FromDate { get; set; }
        public List<ResultEdit> Services { get; set; }

    }
}