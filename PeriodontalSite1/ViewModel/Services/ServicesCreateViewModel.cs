using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel
{
    public class ServicesCreateViewModel
    {

        

        [Required]
        public int ServicesId { get; set; }

        [Display(Name = "Услуга")]
        [Required(ErrorMessage = "{0} должна быть указана")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "{0} должно быть указано")]
        public string Description { get; set; }


        [Display(Name = "Тип")]
        [Required]
        public int TypeId { get; set; }

        [Display(Name = "Еденица измерения")]
        [Required]
        public int UnitId { get; set; }

        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> Units { get; set; }
    }
}