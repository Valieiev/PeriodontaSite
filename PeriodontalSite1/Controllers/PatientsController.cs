using AutoMapper;
using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.Controllers
{
    [Authorize(Roles = "Owner, Reseptionist")]
    public class PatientsController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
        private GenericService<Patients> Patient = new GenericService<Patients>(context);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = Patient.Get().ToList().Map<List<PatientsViewModel>>();

            return View(new PatientsViewModel()
            {
                Patients = result.ToPagedList(pageNumber, pageSize)
            });

        }

        // GET: Dentists/Details/5
        public ActionResult Details(int id)
        {
            PatientsViewModel model = Patient.GetById(id).Map<PatientsViewModel>();

            return View(model);
        }

        // GET: Dentists/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(int id, string redirectUrl)
        {
            var result = Patient.GetById(id);
            Patient.Remove(result);
            return RedirectToLocal(redirectUrl);
        }

        [HttpPost]
        public ActionResult Create(PatientsViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var patient = new Patients
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber
            };

            Patient.Create(patient);

            return RedirectToLocal(redirectUrl);
        }

        public ActionResult Edit(int id)
        {
            PatientsViewModel model = Patient.GetById(id).Map<PatientsViewModel>();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(PatientsViewModel model, string redirectUrl)
        {
            var patient = Patient.GetById(model.PatientsId);
            if (patient != null)
            {
                Mapper.Map(model, patient);
                Patient.Update(patient);
            }

            return RedirectToLocal(redirectUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index), "Patients");
        }


    }
}