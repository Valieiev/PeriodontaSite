using AutoMapper;
using PeriodontalSite1.AutoMapper;
using PeriodontalSite1.AutoMapper.MapProfilers;
using PeriodontalSite1.Migrations;
using PeriodontalSite1.Models;
using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PeriodontalSite1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {


            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<ApplicationContext>(new AppDbInitializer());
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.AddProfile<AppointmentMapProfile>();
                cfg.AddProfile<PriceMapProfiler>();
            }
            );
        }
    }
}
