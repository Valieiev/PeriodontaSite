using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Models
{
    [Table("AppointmentResult")]
    public class AppointmentResult
    {
        [Key]
        public int AppointmentResultId { get; set; }
        public int Count { get; set; }

        public int AppoitmentId { get; set; }

        public int PriceId { get; set; }

        public Appointments Appoitment { get; set; }
        public Prices Price { get; set; }

    }
}