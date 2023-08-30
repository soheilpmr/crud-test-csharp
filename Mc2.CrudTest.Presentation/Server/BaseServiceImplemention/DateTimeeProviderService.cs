namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention
{
    public class DateTimeProviderService : IDateTimeProviderService
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
