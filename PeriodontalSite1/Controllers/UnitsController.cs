using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodontalSite1.Controllers
{
    [Authorize(Roles = "Owner")]
    public class UnitsController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
        private GenericService<Units> Units = new GenericService<Units>(context);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = Units.Get().ToList().Map<List<UnitsViewModel>>();

            return View(new UnitsViewModel()
            {
                Units = result.ToPagedList(pageNumber, pageSize)
            });
       
        }

        // GET: Dentists/Details/5
        public ActionResult Details(int id)
        {
            UnitsViewModel model = Units.GetById(id).Map<UnitsViewModel>();

            return View(model);
        }

        // GET: Dentists/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(int id, string redirectUrl)
        {
            var result = Units.GetById(id);
            Units.Remove(result);
            return RedirectToLocal(redirectUrl);
        }

        [HttpPost]
        public ActionResult Create(UnitsViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Units units = model.Map<Units>();

            Units.Create(units);

            return RedirectToLocal(redirectUrl);
        }

        public ActionResult Edit(int id)
        {
            UnitsViewModel model = Units.GetById(id).Map<UnitsViewModel>();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(UnitsViewModel model, string redirectUrl)
        {
            var unit = Units.GetById(model.UnitsId);
            if (unit != null)
            {
                unit.Name = model.Name;
                unit.Description = model.Description;

                Units.Update(unit);
            }

            return RedirectToLocal(redirectUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index), "Units");
        }




    }
}