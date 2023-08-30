using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.Domain;
using Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository;
using Microsoft.EntityFrameworkCore;

namespace Mc2.Crud.Services.UnitOfWork
{
    public class ServerWriteUnitOfWork<TContext> : UnitOfWorkAsync<TContext>, IServerWriteUnitOfWork
        where TContext : DbContext
    {
        public ServerWriteUnitOfWork(TContext dBContext) : base(dBContext)
        {
            customerWriteRepositoryAsync = new CustomerWriteRepositoryAsync<TContext>(base._context);
        }

        public ICustomerWriteRepositoryAsync customerWriteRepositoryAsync { get; private set; }

        public ICompatibleRepositoryAsync<DomainModel, PrimeryKey> GetRepo<DomainModel, PrimeryKey>()
            where DomainModel : Model<PrimeryKey>
            where PrimeryKey : struct
        {
            ICompatibleRepositoryAsync<DomainModel, PrimeryKey> rtn = null;

            if (typeof(DomainModel) == typeof(Customer))
            {
                rtn = customerWriteRepositoryAsync as ICompatibleRepositoryAsync<DomainModel, PrimeryKey>;
            }
            return rtn;
        }

    }
}
