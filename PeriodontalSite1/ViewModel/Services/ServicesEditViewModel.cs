using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel
{
    public class ServicesEditViewModel
    {
        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> Units { get; set; }

        [Display(Name = "Id")]
        [Required]
        public int ServicesId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }


        [Display(Name = "Type")]
        [Required]
        public int TypeSelected { get; set; }

        [Display(Name = "Unit")]
        [Required]
        public int UnitSelected { get; set; }


    }
}