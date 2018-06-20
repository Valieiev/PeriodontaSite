using PagedList;
using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.ViewModel.AppointmentsResult
{
    public class AppointmentResultStatisticViewModel
    {
        [Display(Name = "Диапазон с:")]
        public DateTime? DateStart { get; set; }
        [Display(Name = "по")]
        public DateTime? DateEnd { get; set; }
        [Display(Name = "Доктор")]
        public string Doctor { get; set; }

        public List<SelectListItem> Users { get; set; }
        public IPagedList<AppointmentResult> AppoitmentRes { get; set; }
    }
}