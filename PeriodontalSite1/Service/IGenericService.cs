using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodontalSite1.Repository
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity GetById(int id);
        IEnumerable<TEntity> Get();
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}