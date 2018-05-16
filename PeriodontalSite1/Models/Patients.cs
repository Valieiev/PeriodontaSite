using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Models
{
    [Table("Patients")]
    public class Patients
    {
        [Key]
        public int PatientsId { get; set; }

        public string FirstName { get; set;  }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        public Patients()
        {
            var Appointments = new List<Appointments>();
        }
    }
}