using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace PeriodontalSite1.Models
{
    [Table("TypeServices")]
    public class TypeServices
    {
        [Key]
        public int TypeServicesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  ICollection<Services> Services { get; set; }

        public TypeServices()
        {
            var Service = new List<Services>();
        }
    }
}