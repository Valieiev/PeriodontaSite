using PagedList;
using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel
{
    public class ServicesViewModel
    {

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
        public TypeServices Type { get; set; }

        [Display(Name = "Unit")]
        [Required]
        public Units Unit { get; set; }


        public IPagedList<ServicesViewModel> Services { get; set; }
    }
}