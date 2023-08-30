using Mc2.CrudTest.Presentation.Server.BaseServiceImplemention.Exceptions;

namespace Mc2.CrudTest.Presentation.Server.Services
{
    public class CustomerException : ServiceException
    {
        public static int ExceptionCode { private set; get; } = 10;
        public CustomerException(string message, Exception innerException) : base(message, innerException, 10)
        {

        }
        public CustomerException(string message) : base(message, 10)
        {

        }


        public CustomerException() : base()
        {
            _code = 10;
        }
    }
}
