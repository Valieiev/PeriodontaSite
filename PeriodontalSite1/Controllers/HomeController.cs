using PagedList;
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
   

    public class HomeController : Controller
    {

        static ApplicationContext context = new ApplicationContext();
        private PriceService Price = new PriceService(context);
        private GenericService<Services> Services = new GenericService<Services>(context);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(int? page)
        {
            var priceList = new List<ResultEdit>();

           DateTime DateTimeFilter = DateTime.Now;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}