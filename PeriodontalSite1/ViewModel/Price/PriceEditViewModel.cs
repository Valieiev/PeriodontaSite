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

        [Required]
        public int ServicesId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime EditFromDate { get; set; }

        public Prices Price { get; set; } 

    }
}