namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention.Exceptions
{
    public class ServiceStorageException : ServiceException
    {
        public ServiceStorageException(string message, Exception innerException)
            : base(message, innerException, 1)
        {
        }
    }
}
