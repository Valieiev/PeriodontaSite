using PeriodontalSite1.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Models
{
    [Table("Appointments")]
    public class Appointments
    {
        [Key]
        public int AppointmentsId { get; set; }
        public DateTime VisitDate { get; set; }
        public Status AppointmentStatus { get; set; }

        public string UserId { get; set; }
     
        public int PatientId { get; set; }

        public ApplicationUser User { get; set; }
        public Patients Patient { get; set; }

        public ICollection<AppointmentResult> Results { get; set; }
        public Appointments()
        {
            var Results = new List<AppointmentResult>();
        }

        public enum Status
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Completed")]
            Completed = 2,
            [Description("Cancelled")]
            Cancelled = 2
        }

    }
}