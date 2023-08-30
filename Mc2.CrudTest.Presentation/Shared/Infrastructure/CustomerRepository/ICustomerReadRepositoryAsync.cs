using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read;
using Mc2.CrudTest.Presentation.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.Infrastructure.CustomerRepository
{
    public interface ICustomerReadRepositoryAsync : IRepositoryReadAsync<Customer, int>
    {
        Task<bool> CheckAnyCompanyExistWithFirstName(string firstName, int id);

        Task<bool> CheckAnyCompanyExistWithLastName(string lastName, int id);

        Task<bool> CheckAnyCompanyExistWithEmail(string email, int id);

        Task<bool> CheckAnyCompanyExistWithDateOfBirth(DateTime dateOfBirth, int id);
        
    }
}
