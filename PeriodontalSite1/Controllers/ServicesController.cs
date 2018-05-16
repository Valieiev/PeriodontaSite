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
    public class ServicesController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
        private ServicesService Services = new ServicesService(context);
        private GenericService<TypeServices> Types = new GenericService<TypeServices>(context);
        private GenericService<Units> Units = new GenericService<Units>(context);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = Services.Get().ToList().Map<List<ServicesViewModel>>();

            return View(new ServicesViewModel()
            {
               Services =  result.ToPagedList(pageNumber, pageSize)
            }
            );
        }

        public ActionResult Details(int id)
        {
            ServicesViewModel result = Services.GetById(id).Map<ServicesViewModel>();
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var units = Units.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.UnitsId)
            }).ToList();
            var types = Types.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.TypeServicesId)
            }).ToList();
            var model = new ServicesCreateViewModel
            {
                Types = types,
                Units = units
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ServicesCreateViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Services service = model.Map<Services>();

            Services.Create(service);

            return RedirectToLocal(redirectUrl);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var units = Units.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.UnitsId)
            }).ToList();
            var types = Types.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.TypeServicesId)
            }).ToList();

            ServicesViewModel var = Services.GetById(id).Map<ServicesViewModel>();
            var model = new ServicesCreateViewModel
            {
                ServicesId = var.ServicesId,
                Name = var.Name,
                Description = var.Description,
                TypeSelected = var.Type.TypeServicesId,
                UnitSelected = var.Unit.UnitsId,
                Types = types,
                Units = units
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ServicesCreateViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model: model);
            }

            var serv = Services.GetById(model.ServicesId);
            if (serv != null)
            {
                serv.Name = model.Name;
                serv.Description = model.Description;
                serv.TypeId = model.TypeSelected;
                serv.UnitId = model.UnitSelected;
                Services.Update(serv);
            }

            return RedirectToLocal(redirectUrl);
        }

        public ActionResult Delete(int id, string redirectUrl)
        {
            var result = Services.GetById(id);
            Services.Remove(result);
            return RedirectToLocal(redirectUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(Index), "Services");
        }

    }
}