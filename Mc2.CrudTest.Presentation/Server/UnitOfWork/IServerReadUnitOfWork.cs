using Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository;

namespace Mc2.Crud.Services.UnitOfWork
{
    public interface IServerReadUnitOfWork : IDynamicTestableUnitOfWorkAsync
    {
        ICustomerReadRepositoryAsync customerReadRepositoryAsync { get; }
    }
}
