using ERM.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERM.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IErmSinkTableRepository ERMSinktable { get; }
    }
}
