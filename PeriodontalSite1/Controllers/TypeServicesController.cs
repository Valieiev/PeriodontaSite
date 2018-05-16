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
    public class TypeServicesController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
        private GenericService<TypeServices> Types = new GenericService<TypeServices>(context);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = Types.Get().ToList().Map<List<TypeServicesViewModel>>();

            return View(new TypeServicesViewModel()
            {
                Types = result.ToPagedList(pageNumber, pageSize)
            });

        }

        // GET: Dentists/Details/5
        public ActionResult Details(int id)
        {
            TypeServicesViewModel model = Types.GetById(id).Map<TypeServicesViewModel>();

            return View(model);
        }

        // GET: Dentists/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(int id, string redirectUrl)
        {
            var result = Types.GetById(id);
            Types.Remove(result);
            return RedirectToLocal(redirectUrl);
        }

        [HttpPost]
        public ActionResult Create(TypeServicesViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TypeServices type = model.Map<TypeServices>();
 
            Types.Create(type);

            return RedirectToLocal(redirectUrl);
        }

        public ActionResult Edit(int id)
        {
            TypeServicesViewModel model = Types.GetById(id).Map<TypeServicesViewModel>();
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(TypeServicesViewModel model, string redirectUrl)
        {
            var unit = Types.GetById(model.TypeServicesId);
            if (unit != null)
            {
                unit.Name = model.Name;
                unit.Description = model.Description;

                Types.Update(unit);
            }

            return RedirectToLocal(redirectUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index), "TypeServices");
        }

    }
}