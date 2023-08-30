using Mc2.CrudTest.Presentation.Shared.BaseServicesImplemetion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC2.Crud.Services.Services
{
    public class DateTimeProviderService : IDateTimeProviderService
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
