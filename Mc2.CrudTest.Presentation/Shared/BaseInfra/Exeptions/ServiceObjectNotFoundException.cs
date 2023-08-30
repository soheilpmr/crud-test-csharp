using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Exeptions
{
    public class ServiceObjectNotFoundException : ServiceException
    {
        public ServiceObjectNotFoundException(string message, Exception innerException) : base(message, innerException, 5)
        {

        }
        public ServiceObjectNotFoundException(string message) : base(message)
        {
            _code = 5;
        }
    }
}
