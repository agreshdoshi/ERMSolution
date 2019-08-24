using ERM.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ERM.DataAccess.Repositories
{
    public class ERMSinkTableRepository : Repository<ErmsinkTable>, IErmSinkTableRepository
    {
        private readonly ERMSinkDbDbContext context;

        public ERMSinkTableRepository(ERMSinkDbDbContext context)
            : base(context)
        {
            this.context = context;
        }
        public  async Task<IEnumerable<ErmsinkTable>> GetAllERMSinkDataAsync()
        {
            return await ERMETLOutputContext.ErmsinkTable.ToListAsync();
        }

        private ERMSinkDbDbContext ERMETLOutputContext
        {
            get { return Context as ERMSinkDbDbContext; }
        }
    }
}
