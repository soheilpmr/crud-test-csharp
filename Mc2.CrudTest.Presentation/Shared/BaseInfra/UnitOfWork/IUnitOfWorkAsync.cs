using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork
{
    public interface IUnitOfWorkAsync : IDisposable
    {
        Task CommitAsync();
    }
}
