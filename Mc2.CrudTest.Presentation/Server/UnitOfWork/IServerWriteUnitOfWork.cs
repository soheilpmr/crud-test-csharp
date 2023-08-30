using Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository;

namespace Mc2.CrudTest.Presentation.Server.UnitOfWork
{
    public interface IServerWriteUnitOfWork : IDynamicTestableUnitOfWorkAsync
    {
        ICustomerWriteRepositoryAsync customerWriteRepositoryAsync { get; }
    }
}
