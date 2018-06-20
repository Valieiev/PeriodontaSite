using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Models.Users;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel.Appointment;
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
    public class AppointmentsController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
    private AppointmentService Appointment = new AppointmentService(context);
    private GenericService<Patients> Patients = new GenericService<Patients>(context);


        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Index(int? page, DateTime? DateStart)
    {
        int pageSize = 20;
            List<AppointmentsViewModel> model = new List<AppointmentsViewModel>();
            DateTime filter = new DateTime();
            if (DateStart != null) {  filter = (DateTime)DateStart; }

        int pageNumber = (page ?? 1);
            if (DateStart != null)
            {
                model = Appointment.Get().Where(a => a.VisitDate.Date == filter.Date).ToList().Map<List<AppointmentsViewModel>>();
            }
            else
            {
                model = Appointment.Get().ToList().Map<List<AppointmentsViewModel>>();
            }
            return View(new AppointmentsViewModel()
        {
            DateStart = DateTime.Now,
            Appointments = model.ToPagedList(pageNumber, pageSize)
        }

         );
    }


        [HttpGet]
        public ActionResult Create()
        {


            var dentists = UserManager.Users.Where(x => x.TypeUser == UserType.Dentist).Select(t => new SelectListItem
            {
                Text = t.FirstName + " " + t.LastName + " " + t.MiddleName,
                Value = t.Id
            }).ToList(); 

            var patients = Patients.Get().Select(s => new SelectListItem
            {
                Text = s.LastName+" "+ s.FirstName+" "+ s.MiddleName,
                Value = Convert.ToString(s.PatientsId)
            }).ToList();

            var model = new AppointmentCreateViewModel
            {
                Users = dentists,
                Patients = patients
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AppointmentCreateViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                var dentists = UserManager.Users.Where(x => x.TypeUser == UserType.Dentist).Select(t => new SelectListItem
                {
                    Text = t.FirstName + " " + t.LastName + " " + t.MiddleName,
                    Value = (t.Id).ToString()
                }).ToList();

                var patients = Patients.Get().Select(s => new SelectListItem
                {
                    Text = s.LastName + " " + s.FirstName + " " + s.MiddleName,
                    Value = Convert.ToString(s.PatientsId)
                }).ToList();
             
                model.Users = patients;
                model.Patients = dentists;
                return View(model);
            }

         
            var app = new Appointments
            {
                PatientId = model.PatientId,
                UserId = model.UserId,
                AppointmentStatus = Status.Pending,
                VisitDate = model.VisitDate     
            };

            Appointment.Create(app);

            return RedirectToLocal(redirectUrl);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
          

            AppointmentsViewModel var = Appointment.GetById(id).Map<AppointmentsViewModel>();
            var model = new AppointmentsViewModel
            {
                AppointmentsId = var.AppointmentsId,
                PatientId = var.Patient.PatientsId,
                UserId = var.User.Id,
                AppointmentStatus = var.AppointmentStatus,
                VisitDate = var.VisitDate
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id, string redirectUrl)
        {


            var app = Appointment.GetById(id);
            Appointment.Remove(app);
             return RedirectToLocal(redirectUrl);
        }



        [HttpPost]
        public ActionResult Edit(EditAppointmentViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model: model);
            }

            var serv = Appointment.GetById(model.AppointmentsId);
            if (serv != null)
            {
                serv.AppointmentStatus = model.AppointmentStatus;
                Appointment.Update(serv);
            }

            return RedirectToLocal(redirectUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Appointments");
        }

    }
}