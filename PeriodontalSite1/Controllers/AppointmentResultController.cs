using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel.AppointmentsResult;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

            var priceList = (from s in context.Services
                        .Include("Types")
                        .Include("Units")
                         select (new ResultEdit { ServicesId = s.ServicesId, Name = s.Name, Price = s.Prices.Where(p => p.FromDate <= DateTime.Now && (p.ToDate == null || p.ToDate > DateTime.Now)).FirstOrDefault() })).ToList();

            var prices = priceList.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.Price.PriceId)
            }).ToList();
            DateTime today = DateTime.Now.Date;
           var  VisitList = (from s in context.Appointments
                       .Include("User")
                       .Include("Patient")
                       .Where ( d=> EntityFunctions.TruncateTime(d.VisitDate) == today)
                         select s).ToList();
            var appointments = VisitList.Select(s => new SelectListItem
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
        public ActionResult Create( AppointmentResultCreateViewModel model, string redirectUrl)
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

     
        public ActionResult Delete(int id, string redirectUrl)
        {
            try
            {
                var rez = AppResult.GetById(id);
                AppResult.Remove(rez);
                return RedirectToLocal(redirectUrl);
            }
            catch
            {
                return RedirectToLocal(redirectUrl);
            }

           
        }


        [HttpGet]
        public ActionResult Statistic(DateTime? StartDate, DateTime? EndDate, string Doctor)
        {
            var model = new AppointmentResultStatisticViewModel();
            if (StartDate != null && EndDate == null && Doctor == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                       .Include("Appoitment")
                       .Where(d => d.Appoitment.VisitDate > StartDate)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
            else if (EndDate != null && StartDate == null && Doctor == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate < EndDate)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
            else if (Doctor != null && StartDate == null && EndDate == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                     .Include("Appoitment")
                     .Where(d => d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
            else if (StartDate != null && EndDate != null && Doctor == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                       .Include("Appoitment")
                       .Where(d => d.Appoitment.VisitDate > StartDate && d.Appoitment.VisitDate < EndDate)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
            else if (StartDate != null && Doctor != null && EndDate == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate > StartDate && d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
            else if (EndDate != null && Doctor != null && StartDate == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate < EndDate && d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
            else if (EndDate != null && Doctor != null && StartDate != null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate > StartDate && d.Appoitment.VisitDate < EndDate && d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            } else if (EndDate == null && Doctor == null && StartDate == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                                 select s).ToList();
                model.AppoitmentRes = VisitList;
            }
           
            return View(model);
        }



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