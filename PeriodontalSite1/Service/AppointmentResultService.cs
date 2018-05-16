using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PeriodontalSite1.Models;

namespace PeriodontalSite1.Repository
{
    public class AppointmentResultService : GenericService<AppointmentResult>
    {
        public AppointmentResultService(ApplicationContext context) : base(context)
        {
        }

        public override IEnumerable<AppointmentResult> Get()
        {

             return dbSet.Include(i => i.Price).Include(i => i.Price.Services).Include(d => d.Appoitment).Include(i => i.Appoitment.Patient).Include(i => i.Appoitment.User).ToList();

        }
        public override AppointmentResult GetById(int id)
        {
            return dbSet.Include(i => i.Price).Include(d => d.Appoitment).FirstOrDefault(f => f.AppointmentResultId == id);
      
        }
    }
}