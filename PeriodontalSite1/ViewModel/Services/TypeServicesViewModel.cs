using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel
{
    public class TypeServicesViewModel
    {
        [Display(Name = "Id")]
        [Required]
        public int TypeServicesId { get; set; }
        [Display(Name = "Услгуа")]
        [Required(ErrorMessage = "{0} должна быть указана")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "{0} должно быть указано")]
        public string Description { get; set; }
        public IPagedList<TypeServicesViewModel> Types { get; set; }


    }
}