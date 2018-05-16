

using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using PeriodontalSite1.Models;

namespace PeriodontalSite1.Repository
{
    public class PriceService : GenericService<Prices>
    {
        public PriceService(ApplicationContext context) : base(context)
        {
        }

        public override IEnumerable<Prices> Get()
        {

            return dbSet.Include(i => i.Services).ToList();
        }
        public override Prices GetById(int id)
        {
            return dbSet.Include(i => i.Services).FirstOrDefault(f => f.PriceId == id);
        }
    }
}