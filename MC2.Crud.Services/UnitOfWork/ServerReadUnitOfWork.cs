
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.Domain;
using Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository;
using Microsoft.EntityFrameworkCore;

namespace Mc2.Crud.Services.UnitOfWork
{
    public class ServerReadUnitOfWork<TContext> : UnitOfWorkAsync<TContext>, IServerReadUnitOfWork
        where TContext : DbContext
    {
        public ServerReadUnitOfWork(TContext dBContext) : base(dBContext)
        {
            customerReadRepositoryAsync = new CustomerReadRepositoryAsync<TContext>(base._context);
        }

        public ICustomerReadRepositoryAsync customerReadRepositoryAsync { get; private set; }

        public ICompatibleRepositoryAsync<DomainModel, PrimeryKey> GetRepo<DomainModel, PrimeryKey>()
            where DomainModel : Model<PrimeryKey>
            where PrimeryKey : struct
        {
            ICompatibleRepositoryAsync<DomainModel, PrimeryKey> rtn = null;

            if (typeof(DomainModel) == typeof(Customer))
            {
                rtn = customerReadRepositoryAsync as ICompatibleRepositoryAsync<DomainModel, PrimeryKey>;
            }
            return rtn;
        }
    }
}
