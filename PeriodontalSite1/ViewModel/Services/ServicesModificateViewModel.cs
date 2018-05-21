using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.ViewModel.Services
{
    public class ServicesModificateViewModel
    {
        public int ServicesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }
    }
}