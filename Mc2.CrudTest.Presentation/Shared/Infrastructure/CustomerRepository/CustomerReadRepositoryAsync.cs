using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read;
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
    public class CustomerReadRepositoryAsync<TContext> : RepositoryReadAsync<CustomerEntity, Customer, int, TContext>, ICustomerReadRepositoryAsync
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        public CustomerReadRepositoryAsync(TContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> CheckAnyCompanyExistWithFirstName(string firstName, int id)
        {
            return await _entities.AnyAsync(ss => ss.FirstName == firstName && ss.ID != id);
        }

        public async Task<bool> CheckAnyCompanyExistWithLastName(string lastName, int id)
        {
            return await _entities.AnyAsync(ss => ss.LastName == lastName && ss.ID != id);
        }

        public async Task<bool> CheckAnyCompanyExistWithEmail(string email, int id)
        {
            return await _entities.AnyAsync(ss => ss.Email == email && ss.ID != id);
        }

        public async Task<bool> CheckAnyCompanyExistWithDateOfBirth(DateTime dateOfBirth, int id)
        {
            return await _entities.AnyAsync(ss => ss.DateOfBirth == dateOfBirth && ss.ID != id);
        }
    }
}
