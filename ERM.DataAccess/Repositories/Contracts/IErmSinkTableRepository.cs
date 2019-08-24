using ERM.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERM.DataAccess.Repositories
{
    public interface IErmSinkTableRepository: IRepository<ErmsinkTable>
    {
        Task<IEnumerable<ErmsinkTable>> GetAllERMSinkDataAsync();
    }
}
