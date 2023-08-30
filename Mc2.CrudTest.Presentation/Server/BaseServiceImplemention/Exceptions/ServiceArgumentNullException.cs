namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention.Exceptions
{
    public class ServiceArgumentNullException : ServiceException
    {
        public ServiceArgumentNullException(string message, Exception innerException)
            : base(message, innerException, 2)
        {
        }

        public ServiceArgumentNullException(string message)
            : base(message)
        {
            _code = 2;
        }
    }
}
