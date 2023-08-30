using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Exeptions
{
    public class ServiceException : Exception
    {
        protected int? _code;

        public int? code => -code;

        public ServiceException() : base()
        {

        }

        public ServiceException(string message) : base(message)
        {

        }

        public ServiceException(string message, int code)
            : base(message)
        {
            _code = code;
        }

        public ServiceException(string message, Exception innerException, int code)
           : base(message, innerException)
        {
            _code = code;
        }

        public ServiceException(SerializationInfo info, StreamingContext context, int code)
          : base(info, context)
        {
            _code = code;
        }

        public string ToServiceExceptionString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (_code.HasValue)
            {
                stringBuilder.Append("service_exception_code:" + _code);
            }

            if (Message != null)
            {
                stringBuilder.Append(",service_exception_message:" + Message);
            }

            Exception ex = this;
            int num = 1;
            while (ex.InnerException != null)
            {
                stringBuilder.Append(",level_" + num + ":" + ex.InnerException!.Message);
                num++;
                ex = ex.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
