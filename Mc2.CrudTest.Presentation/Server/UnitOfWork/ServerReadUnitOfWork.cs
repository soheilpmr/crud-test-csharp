using Mc2.CrudTest.Presentation.Server.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Context;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.Domain;
using Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mc2.CrudTest.Presentation.Server.UnitOfWork
{
    public class ServerReadUnitOfWork : UnitOfWorkAsync<ReadDBContext>, IServerReadUnitOfWork
    {
        public ServerReadUnitOfWork(ReadDBContext dBContext) : base(dBContext)
        {
            customerReadRepositoryAsync = new CustomerReadRepositoryAsync(base._context);
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
