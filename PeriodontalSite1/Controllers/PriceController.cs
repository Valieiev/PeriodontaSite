using AutoMapper;
using PagedList;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.Models;
using PeriodontalSite1.Repository;
using PeriodontalSite1.ViewModel;
using PeriodontalSite1.ViewModel.Price;
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

        public ActionResult Index(int? page, DateTime? DateTimeFilter)
        {
            var priceList = new List<ResultEdit>();

                if (DateTimeFilter == null) DateTimeFilter = DateTime.Now;

             priceList = (from s in context.Services
                        .Include("Types")
                        .Include("Units")
                           select (new ResultEdit { ServicesId = s.ServicesId, Name = s.Name, Price = s.Prices.Where(p => p.FromDate <= DateTimeFilter && (p.ToDate == null || p.ToDate > DateTimeFilter)).FirstOrDefault() })).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(new PriceViewModel()
            {
                EditFromDate = DateTimeFilter,
                DateTimeFilter = DateTimeFilter,
                Prices = priceList.ToPagedList(pageNumber, pageSize)
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

            List<Prices> prices = Price.Get().Where(t => t.ServicesId == model.ServiceSelected).ToList().Where(p=>p.FromDate <= model.FromDate  &&  (p.ToDate > model.FromDate || p.ToDate == null )).ToList();
   
            if (prices.Count != 0)
            {
                prices[0].ToDate = model.FromDate;
                Price.Update(prices[0]);
            }

            var price = new Prices
            {
                ServicesId = model.ServiceSelected,
                ToDate = null,
                Value = model.Value,
                FromDate = model.FromDate
            };

            if (prices.Count == 2)
            {
                price.ToDate = prices[1].FromDate;
            }
   
            Price.Create(price);

            return RedirectToLocal(redirectUrl);
        }


        [HttpGet]
        public ActionResult Edit(DateTime EditFromDate)
        {
            if (EditFromDate < DateTime.Now) EditFromDate = DateTime.Now;
            var service = (from s in context.Services
                           .Include("Types")
                           .Include("Units")
                           select (new ResultEdit { ServicesId = s.ServicesId,  Name = s.Name, Price = s.Prices.Where(p=> p.FromDate <= EditFromDate && (p.ToDate == null || p.ToDate > EditFromDate)).FirstOrDefault()})).ToList();

            var model = Mapper.Map<List<PriceEditViewModel>>(service);
            foreach (var item in model)
            {
                item.EditFromDate = EditFromDate;
                if(item.Price != null)   item.Price.FromDate = EditFromDate;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(List<PriceEditViewModel> model, string redirectUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
          
            foreach (var item in model)
            {
                if (item.Price.PriceId == 0)
                {
                    Prices price = new Prices()
                    {
                        Value = item.Price.Value,
                        FromDate = item.EditFromDate,
                        ServicesId = item.ServicesId,
                        ToDate = null,

                    };
                    Price.Create(price);
                }
                else
                {
                    Prices price = new Prices();
                    var PREV = (from s in context.Services
                            .Include("Types")
                            .Include("Units")
                                   select (new ConfirmEdit { ServicesId = s.ServicesId, Name = s.Name, Prices = s.Prices.Where(p => p.FromDate < item.EditFromDate && (p.ToDate == null || p.ToDate > item.EditFromDate)).OrderBy(p=>p.FromDate).FirstOrDefault() })).Where(s=>s.ServicesId == item.ServicesId).ToList();
                    var NEXT = (from s in context.Services
                            .Include("Types")
                            .Include("Units")
                                select (new ConfirmEdit { ServicesId = s.ServicesId, Name = s.Name, Prices = s.Prices.Where(p => p.FromDate > item.EditFromDate && (p.ToDate == null || p.ToDate > item.EditFromDate)).FirstOrDefault() })).Where(s => s.ServicesId == item.ServicesId).ToList();
                    if (PREV[0].Prices != null)
                    {
                        Prices prev =  Price.GetById(PREV[0].Prices.PriceId);
                        prev.ToDate = item.EditFromDate;
                        Price.Update(prev);
                    }
                    if (NEXT[0].Prices != null)
                    {
                        Prices next = Price.GetById(NEXT[0].Prices.PriceId);
                        price.ToDate = next.FromDate;
                    }

                    price.Value = item.Price.Value;
                    price.FromDate = item.EditFromDate;
                    price.ServicesId = item.ServicesId;
                    Price.Create(price);
                    
                }
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
            return RedirectToAction(nameof(Index), "Price");
        }
        
    }
}