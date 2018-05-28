using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodontalSite1.Models
{
    public class Prices
    {
        [Key]
        public int PriceId { get; set; }
        public double Value { get; set; }
        public DateTime FromDate { get; set; }
     
        public DateTime? ToDate { get; set; }

        public int ServiceId { get; set; }
        public Services Services { get; set; }

        public ICollection<AppointmentResult> Results { get; set; }
        public Prices()
        {
            var Results = new List<AppointmentResult>();
        }
    }
}