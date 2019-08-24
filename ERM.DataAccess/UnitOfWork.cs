using System;
using ERM.DataAccess.Models;
using ERM.DataAccess.Repositories;

namespace ERM.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ERMSinkDbDbContext context;

        public UnitOfWork(ERMSinkDbDbContext context, IErmSinkTableRepository repository)
        {
            this.context = context;
            ERMSinktable = new ERMSinkTableRepository(context);
        }
        public IErmSinkTableRepository ERMSinktable { get; private set; }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
