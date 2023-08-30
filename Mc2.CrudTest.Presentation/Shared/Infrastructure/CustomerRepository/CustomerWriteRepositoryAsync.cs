using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Write;
using Mc2.CrudTest.Presentation.Shared.Data;
using Mc2.CrudTest.Presentation.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository
{
    public class CustomerWriteRepositoryAsync<TContext> : RepositoryWriteAsync<CustomerEntity, Customer, int, TContext>, ICustomerWriteRepositoryAsync
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        public CustomerWriteRepositoryAsync(TContext dBContext) : base(dBContext) 
        {
             _dbContext = dBContext;
        }
    }
}
