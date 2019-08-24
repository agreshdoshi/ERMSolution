using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERM.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
       Task<IEnumerable<TEntity>> GetAll();
    }
}
