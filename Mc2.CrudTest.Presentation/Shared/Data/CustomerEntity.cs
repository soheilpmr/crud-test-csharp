using Mc2.CrudTest.Presentation.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.Data
{
    public class CustomerEntity : Customer
    {
        public CustomerEntity()
        {

        }

        public CustomerEntity(Customer customer)
        {
            ID = customer.ID;
            LastName = customer.LastName;
            FirstName = customer.FirstName;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            BankAccountNumber = customer.BankAccountNumber;
            DateOfBirth = customer.DateOfBirth;
        }
    }
}
