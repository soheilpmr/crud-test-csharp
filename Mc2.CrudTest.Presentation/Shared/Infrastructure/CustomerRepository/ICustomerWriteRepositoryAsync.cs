using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Write;
using Mc2.CrudTest.Presentation.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository
{
    public interface ICustomerWriteRepositoryAsync : IRepositoryWriteAsync<Customer, int>
    {

    }
}
