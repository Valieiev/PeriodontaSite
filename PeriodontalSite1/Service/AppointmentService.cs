using PeriodontalSite1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace PeriodontalSite1.Repository
{
    public class AppointmentService : GenericService<Appointments>
    {
        public AppointmentService(ApplicationContext context) : base(context)
        {
        }

        public override IEnumerable<Appointments> Get()
        {

              return dbSet.Include(i => i.Patient).Include(i =>i.User).ToList();
           
        }
        public override Appointments GetById(int id)
        {
            return dbSet.Include(i => i.Patient).Include(i => i.User).FirstOrDefault(f => f.AppointmentsId == id);
           
        }
    }
}