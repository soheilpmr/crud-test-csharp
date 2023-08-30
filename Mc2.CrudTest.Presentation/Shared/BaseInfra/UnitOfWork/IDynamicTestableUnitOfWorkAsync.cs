using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork
{
    public interface IDynamicTestableUnitOfWorkAsync : IUnitOfWorkAsync
    {
        ICompatibleRepositoryAsync<DomainModel, PrimeryKey> GetRepo<DomainModel, PrimeryKey>()
        where DomainModel : Model<PrimeryKey>
        where PrimeryKey : struct;
    }
}
