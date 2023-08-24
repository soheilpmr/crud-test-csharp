using System.Text;

namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention
{
    public abstract class BaseService<LogDomain>
    {
        private readonly ILogger<LogDomain> _logger;
        public BaseService(ILogger<LogDomain> logger)
        {
            this._logger = logger;
        }

        protected static string GetExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            if (ex != null)
            {
                sb.Append(ex.Message);
            }

            Exception inner = ex?.InnerException;
            int counter = 1;
            while (inner != null)
            {
                sb.Append(" ,InnerException" + counter.ToString() + " Message:" + inner.Message);
                inner = inner.InnerException;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="details"></param>
        /// <param name="objectID"></param>
        /// <param name="logType"></param>
        protected void Log(EventId? eventId, string details, string? objectID = null, LogEnumType logType = LogEnumType.SuccessLog)
        {
            //If datials has some {} it will be regarded as log parameter wich is not ok
            details = details.Replace("{", "[");
            details = details.Replace("}", "]");
            if (logType == LogEnumType.ErrorLog)
            {
                if (eventId == null)
                {
                    _logger.LogError(message: details + " ,ObjectID:{ObjectID},logTypeID:{LogTypeID}", objectID, logType);
                }
                else
                {
                    _logger.LogError(eventId: eventId.Value, message: details + " ,ActionID:{ActionID},ObjectID:{ObjectID},logTypeID:{LogTypeID}", eventId.Value, objectID, logType);
                }
            }
            else
            {
                if (eventId == null)
                {
                    _logger.LogInformation(message: details + " ,ObjectID:{ObjectID},logTypeID:{LogTypeID}", objectID, logType);
                }
                else
                {
                    _logger.LogInformation(eventId.Value, message: details + " ,ActionID:{ActionID},ObjectID:{ObjectID},logTypeID:{LogTypeID}", eventId.Value, objectID, logType);
                }
            }
        }
    }
}
