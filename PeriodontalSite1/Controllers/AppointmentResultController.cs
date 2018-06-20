using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Models.Users;
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
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var result = AppResult.Get().OrderByDescending(d=>d.Appoitment.VisitDate).ToList().Map<List<AppointmentResultViewModel>>();
            


            DateTime today = DateTime.Now.Date;
            var VisitList = (from s in context.Appointments
                     .Include("User")
                     .Include("Patient")
                     .Where(d => EntityFunctions.TruncateTime(d.VisitDate) == today)
                             select s).ToList();
            var appointments = VisitList.Select(s => new SelectListItem
            {
                Text = "Доктор:" + s.User.FirstName + " " + s.User.LastName + " Пациент:" + s.Patient.FirstName + " " + s.Patient.LastName + " Дата:" + s.VisitDate,
                Value = Convert.ToString(s.AppointmentsId)
            }).ToList();


            return View(new AppointmentResultViewModel()
            {
                AppoitmentList = appointments,
              // CreatedAppoitmentId = Convert.ToInt32(appointments[0].Value),
                 AppointmentResults = result.ToPagedList(pageNumber, pageSize)
            }
            );
        }

        [HttpGet]
        public ActionResult Create(int? AppoitmentId, int? Modelcount)
        {
           
            if (AppoitmentId == 0 || AppoitmentId == null)
            {
                return HttpNotFound();
            }
            var priceList = (from s in context.Services
                        .Include("Types")
                        .Include("Units")
                         select (new ResultEdit { ServicesId = s.ServicesId, Name = s.Name, Price = s.Prices.Where(p => p.FromDate <= DateTime.Now && (p.ToDate == null || p.ToDate > DateTime.Now)).FirstOrDefault() })).ToList();

            var prices = priceList.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.Price.PriceId)
            }).ToList();
            if (Modelcount == null) Modelcount = 1;
            else Modelcount = Modelcount + 1;
            var model = new List<AppointmentResultCreateViewModel>();
            for (int i = 0; i < Modelcount; i++)
            {
                model.Add(new AppointmentResultCreateViewModel() { AppoitmentId = AppoitmentId, Price = prices });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create( List<AppointmentResultCreateViewModel> model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            foreach (var item in model)
            {
                var result = new AppointmentResult
                {

                    AppoitmentId = Convert.ToInt32(item.AppoitmentId),
                    PriceId = item.PriceId,
                    Count = item.Count
                   
                };
                AppResult.Create(result);
            }

            var app = Appointment.GetById(Convert.ToInt32(model[0].AppoitmentId));
                app.AppointmentStatus = Status.Completed;
                Appointment.Update(app);



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
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [HttpGet]
        public ActionResult Statistic(int? page ,DateTime? DateStart, DateTime? DateEnd, string Doctor)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);

            var dentists = UserManager.Users.Where(x => x.TypeUser == UserType.Dentist).Select(t => new SelectListItem
            {
                Text = t.FirstName + " " + t.LastName + " " + t.MiddleName,
                Value = t.Id
            }).ToList();
            dentists.Add(null);
            dentists.Reverse();
            if (Doctor == "") Doctor = null;
            var model = new AppointmentResultStatisticViewModel();
            if (DateStart != null && DateEnd == null && Doctor == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                       .Include("Appoitment")
                       .Where(d => d.Appoitment.VisitDate >= DateStart)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            else if (DateEnd != null && DateStart == null && Doctor == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate <= DateEnd)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            else if (Doctor != null && DateStart == null && DateEnd == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                     .Include("Appoitment")
                     .Where(d => d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            else if (DateStart != null && DateEnd != null && Doctor == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                       .Include("Appoitment")
                       .Where(d => d.Appoitment.VisitDate >= DateStart && d.Appoitment.VisitDate <= DateEnd)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            else if (DateStart != null && Doctor != null && DateEnd == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate >= DateStart && d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            else if (DateEnd != null && Doctor != null && DateStart == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate <= DateEnd && d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            else if (DateEnd != null && Doctor != null && DateStart != null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                      .Where(d => d.Appoitment.VisitDate >= DateStart && d.Appoitment.VisitDate <= DateEnd && d.Appoitment.UserId == Doctor)
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            } else if (DateEnd == null && Doctor == null && DateStart == null)
            {
                var VisitList = (from s in context.AppointmentsResult
                      .Include("Appoitment")
                                 select s).ToList();
                model.AppoitmentRes = VisitList.ToPagedList(pageNumber, pageSize);
            }
            model.Users = dentists;


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