using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel.AppointmentsResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PeriodontalSite1.Models.Appointments;

namespace PeriodontalSite1.Controllers
{
    [Authorize(Roles = "Owner, Reseptionist")]
    public class AppointmentResultController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
        private AppointmentService Appointment = new AppointmentService(context);
        private PriceService Price = new PriceService(context);
        private AppointmentResultService AppResult = new AppointmentResultService(context);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = AppResult.Get().ToList().Map<List<AppointmentResultViewModel>>();

            return View(new AppointmentResultViewModel()
            {
                 AppointmentResults = result.ToPagedList(pageNumber, pageSize)
            }
            );
        }

        [HttpGet]
        public ActionResult Create()
        {
            var prices = Price.Get().Select(s => new SelectListItem
            {
               Text = s.Services.Name,
                Value = Convert.ToString(s.PriceId)
            }).ToList();
            var appointments = Appointment.Get().Select(s => new SelectListItem
            {
                Text = "Доктор:"+s.User.FirstName+" "+s.User.LastName+ " Пациент:"+ s.Patient.FirstName+" "+ s.Patient.LastName+" Дата:"+s.VisitDate,
                Value = Convert.ToString(s.AppointmentsId)
            }).ToList();
            var model = new AppointmentResultCreateViewModel
            {
                Appoitment = appointments,
                Price = prices
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AppointmentResultCreateViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = new AppointmentResult
            {
                Count = model.Count,
                AppoitmentId = model.AppoitmentId,
                PriceId = model.PriceId
            };
            var app = Appointment.GetById(model.AppoitmentId);
            app.AppointmentStatus = Status.Completed;
            Appointment.Update(app);
            AppResult.Create(result);

            return RedirectToLocal(redirectUrl);
        }


        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var units = Price.Get().Select(s => new SelectListItem
        //    {
        //        Text = s.Services.Name,
        //        Value = Convert.ToString(s.PriceId)
        //    }).ToList();
        //    var types = Types.Get().Select(s => new SelectListItem
        //    {
        //        Text = s.Name,
        //        Value = Convert.ToString(s.TypeServicesId)
        //    }).ToList();

        //    ServicesViewModel var = Services.GetById(id).Map<ServicesViewModel>();
        //    var model = new ServicesCreateViewModel
        //    {
        //        ServicesId = var.ServicesId,
        //        Name = var.Name,
        //        Description = var.Description,
        //        TypeSelected = var.Type.TypeServicesId,
        //        UnitSelected = var.Unit.UnitsId,
        //        Types = types,
        //        Units = units
        //    };
        //    return View(model);
        //}

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "AppointmentResult");
        }
    }
 }