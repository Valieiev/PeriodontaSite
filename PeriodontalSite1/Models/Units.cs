using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Models
{
    [Table("Units")]
    public class Units
    {
        [Key]
        public int UnitsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  ICollection<Services> Services { get; set; }
        public Units()
        {
            var Service = new List<Services>();
        }
    }
}