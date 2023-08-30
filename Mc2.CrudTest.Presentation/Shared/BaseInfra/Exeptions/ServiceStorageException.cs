using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Exeptions
{
    public class ServiceStorageException : ServiceException
    {
        public ServiceStorageException(string message, Exception innerException) : base(message, innerException, 1)
        {

        }
    }
}
