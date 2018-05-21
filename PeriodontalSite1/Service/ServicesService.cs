
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PeriodontalSite1.Models;

namespace PeriodontalSite1.Repository
{
    public class ServicesService : GenericService<Services>
    {
        public ServicesService(ApplicationContext context) : base(context)
        {
        }

        public override IEnumerable<Services> Get()
        {

            return dbSet.Include(i => i.Types).Include(d => d.Units).ToList();
        }
        public override Services GetById(int id)
        { 
            return dbSet.Include(i => i.Types).Include(d => d.Units).FirstOrDefault(f => f.ServicesId == id);
        }
    }
}