using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PeriodontalSite1.Controllers
{
    [Authorize(Roles = "Owner")]
    public class PriceController : Controller
    {
        static ApplicationContext context = new ApplicationContext();
        private PriceService Price = new PriceService(context);
        private GenericService<Services> Services = new GenericService<Services>(context);

        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var result = Price.Get().ToList().Map<List<PriceViewModel>>();

            return View(new PriceViewModel()
            {
                Prices = result.ToPagedList(pageNumber, pageSize)
            }
            );
        }


       [HttpGet]
        public ActionResult Create()
        {
            var services = Services.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.ServicesId)
            }).ToList();
           
            var model = new PriceCreateViewModel
            {
                Service = services
      
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PriceCreateViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
              return View(model);
            }

            var price = new Prices
            {
                ServiceId = model.ServiceSelected,
                Value = model.Value,
                ToDate = model.ToDate,
                FromDate = model.FromDate
            };

            Price.Create(price);

            return RedirectToLocal(redirectUrl);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var services = Services.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = Convert.ToString(s.ServicesId)
            }).ToList();
 
            PriceViewModel var = Price.GetById(id).Map<PriceViewModel>();
            var model = new PriceCreateViewModel
            {
                PriceId = var.PriceId,
                Value = var.Value,
                ToDate = var.ToDate,
                FromDate = var.FromDate,
                ServiceSelected = var.Services.ServicesId,
                Service = services,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PriceCreateViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model: model);
            }

            var serv = Price.GetById(model.PriceId);
            if (serv != null)
            {
                serv.ServiceId = model.ServiceSelected;
                serv.Value = model.Value;
                serv.FromDate = model.FromDate;
                serv.ToDate = model.ToDate;
                Price.Update(serv);
            }

            return RedirectToLocal(redirectUrl);
        }

        public ActionResult Delete(int id, string redirectUrl)
        {
            var result = Price.GetById(id);
            Price.Remove(result);
            return RedirectToLocal(redirectUrl);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Price");
        }
    }
}