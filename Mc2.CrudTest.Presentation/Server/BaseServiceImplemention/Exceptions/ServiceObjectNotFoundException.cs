namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention.Exceptions
{
    public class ServiceObjectNotFoundException : ServiceException
    {
        public ServiceObjectNotFoundException(string message, Exception innerException) : base(message, innerException, 5)
        {

        }
        public ServiceObjectNotFoundException(string message) : base(message)
        {
            _code = 1;
        }
    }
}
