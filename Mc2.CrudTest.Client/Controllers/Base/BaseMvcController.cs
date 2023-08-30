using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Client.Controllers.Base
{
    public class BaseMvcController<TService> : Controller
        where TService : class
    {
        protected readonly ILogger<TService> _logger;
        protected const string MessageAccessLogSuccessfully = "Successfully Access To This Method";
        protected const string MessageSuccessfullyDoneLogMethod = "Successfully Finished This Request And response sent to the User";
        protected readonly int _baseActionID;

        public BaseMvcController(ILogger<TService> logger, int baseActionID)
        {
            _logger = logger;
            _baseActionID = baseActionID;
        }
        protected void LogMultipleGetFailure(Exception exception, string? methodName = null, string? message = null)
        {
            LogMultipleGet(LogPurposeType.Failure, methodName, message, null, exception);
        }
        protected void LogMultipleGet(LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            string s = "";
            if (message == null)
            {

                if (purpose == LogPurposeType.Success)
                {
                    s = "Successfully called a controller method to get multiple items of service" + typeof(TService).FullName;
                }
                else
                {
                    s = "Failed to called a controller method to get multiple items of service" + typeof(TService).FullName;
                }
            }
            else
            {
                s = message;
            }
            Log(purpose, methodName, s, code ?? 1, exception);
        }
        protected void LogPostFailure(string message)
        {
            string s = "Failed to called a controller method to post to service" + typeof(TService).FullName;
            s += ",reason:" + message;
            LogPost(LogPurposeType.Failure, null, s);
        }
        protected void LogPostFailure(Exception exception)
        {
            LogPost(LogPurposeType.Failure, null, null, null, exception);
        }

        protected void LogPost(LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            string s = "";
            if (message == null)
            {

                if (purpose == LogPurposeType.Success)
                {
                    s = "Successfully called a controller method to post to service" + typeof(TService).FullName;
                }
                else
                {
                    s = "Failed to called a controller method to post to service" + typeof(TService).FullName;
                }
            }
            else
            {
                s = message;
            }
            Log(purpose, methodName, s, code ?? 1, exception);
        }
        protected void LogPutFailure(string message)
        {
            string s = "Failed to called a controller method to put to service" + typeof(TService).FullName;
            s += ",reason:" + message;
            LogPut(LogPurposeType.Failure, null, s);
        }
        protected void LogPutFailure(Exception exception)
        {
            LogPut(LogPurposeType.Failure, null, null, null, exception);
        }
        protected void LogPut(LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            string s = "";
            if (message == null)
            {

                if (purpose == LogPurposeType.Success)
                {
                    s = "Successfully called a controller method to put to service" + typeof(TService).FullName;
                }
                else
                {
                    s = "Failed to called a controller method to put to service" + typeof(TService).FullName;
                }
            }
            else
            {
                s = message;
            }
            Log(purpose, methodName, s, code ?? 3, exception);
        }
        protected void LogDeleteFailure(string message)
        {
            string s = "Failed to called a controller method to delete to service" + typeof(TService).FullName;
            s += ",reason:" + message;
            LogDelete(LogPurposeType.Failure, null, s);
        }
        protected void LogDeleteFailure(Exception exception)
        {
            LogDelete(LogPurposeType.Failure, null, null, null, exception);
        }
        protected void LogDelete(LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            string s = "";
            if (message == null)
            {

                if (purpose == LogPurposeType.Success)
                {
                    s = "Successfully called a controller method to delete to service" + typeof(TService).FullName;
                }
                else
                {
                    s = "Failed to called a controller method to delete to service" + typeof(TService).FullName;
                }
            }
            else
            {
                s = message;
            }
            Log(purpose, methodName, s, code ?? 4, exception);
        }

        protected void LogRetrieveSingleFailure(string message)
        {
            string s = "Failed to called a controller method to retrieve single item from service" + typeof(TService).FullName;
            s += ",reason:" + message;
            LogRetrieveSingle(LogPurposeType.Failure, null, s);
        }
        protected void LogRetrieveSingleFailure(Exception exception)
        {
            LogRetrieveSingle(LogPurposeType.Failure, null, null, null, exception);
        }
        protected void LogRetrieveSingle(LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            string s = "";
            if (message == null)
            {

                if (purpose == LogPurposeType.Success)
                {
                    s = "Successfully called a controller method to retrieve single item from service" + typeof(TService).FullName;
                }
                else
                {
                    s = "Failed to called a controller method to retrieve single item from service" + typeof(TService).FullName;
                }
            }
            else
            {
                s = message;
            }
            Log(purpose, methodName, s, code ?? 2, exception);
        }

        protected void Log(LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            Log(_logger, purpose, methodName, message, code, exception);
        }

        private void Log(ILogger<TService> logger, LogPurposeType purpose, string? methodName = null, string? message = null, int? code = null, Exception? exception = null)
        {
            string fromMethod = "";
            string requestDetails = "";
            string details;
            if (!string.IsNullOrEmpty(methodName))
            {
                fromMethod = " from method: '" + methodName + "' ";
            }
            if (!string.IsNullOrEmpty(message))
            {
                details = message;
            }
            else
            {
                details = "Accessed to get multiple items of service " + typeof(TService).FullName;
            }
            if (purpose == LogPurposeType.Failure)
            {
                if (exception != null)
                {
                    logger.LogError(exception, details + fromMethod + requestDetails + " logcode:{UserActionID}", _baseActionID + (code ?? 5));
                }
                else
                {
                    logger.LogError(details + fromMethod + requestDetails + " logcode:{UserActionID}", _baseActionID + (code ?? 5));
                }
            }
            else
            {
                logger.LogInformation(details + fromMethod + requestDetails + " logcode:{UserActionID}", _baseActionID + (code ?? 5));
            }
        }

    }
}
