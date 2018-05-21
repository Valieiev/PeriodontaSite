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
        [Required]
        public int ServicesId { get; set; }

        [Display(Name = "Услуга")]
        [Required(ErrorMessage = "{0} должна быть указана")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "{0} должно быть указано")]
        public string Description { get; set; }


        [Display(Name = "Тип")]
        [Required(ErrorMessage = "{0} должен быть указан")]
        public TypeServices Types { get; set; }

        [Display(Name = "Еденица измерения")]
        [Required(ErrorMessage = "{0} должна быть указана")]
        public Units Units { get; set; }


        public IPagedList<ServicesViewModel> Services { get; set; }
    }
}