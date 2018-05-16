using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel
{
    public class UnitsViewModel
    {
        [Display(Name = "Id")]
        [Required]
        public int UnitsId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        public IPagedList<UnitsViewModel> Units { get; set; }

      
    }
}