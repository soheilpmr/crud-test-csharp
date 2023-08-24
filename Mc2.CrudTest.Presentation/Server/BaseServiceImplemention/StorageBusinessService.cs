using Mc2.CrudTest.Presentation.Server.BaseServiceImplemention.Exceptions;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using System.Text;

namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention
{
    public abstract class StorageBusinessService<DomainModel, PrimaryKeyType> : BaseService<DomainModel>, IStorageBusinessService<DomainModel, PrimaryKeyType>
      where DomainModel : Model<PrimaryKeyType>
      where PrimaryKeyType : struct
    {

        protected readonly IDateTimeProviderService _dateTimeProvider;
        protected readonly int _logBaseID;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dateTimeProvider"></param>
        /// <param name="logBaseID">
        /// logBaseID+1 =Add
        /// logBaseID+2 =Retrieve single
        /// logBaseID+3 =Modify
        /// logBaseID+4= Remove
        /// logBaseID+5 =Retrieve multiple
        /// </param>
        public StorageBusinessService(ILogger<DomainModel> logger, IDateTimeProviderService dateTimeProvider, int logBaseID) : base(logger)
        {

            _dateTimeProvider = dateTimeProvider;
            _logBaseID = logBaseID;
        }


        public abstract Task<PrimaryKeyType> AddAsync(DomainModel item);
        public abstract Task<List<DomainModel>> ItemsAsync();
        public abstract Task ModifyAsync(DomainModel item);
        public abstract Task RemoveByIdAsync(PrimaryKeyType ID);
        public abstract Task<DomainModel> RetrieveByIdAsync(PrimaryKeyType ID);


        #region logging
        /// <summary>
        /// logBaseID+1 =Add
        /// logBaseID+2 =Retrieve single
        /// logBaseID+3 =Modify
        /// logBaseID+4= Remove
        /// logBaseID+5 =Retrieve multiple
        /// </summary>
        public int LogBaseID { get { return _logBaseID; } }
        protected void LogAdd(DomainModel model, string objectDescriptor = "", Exception? ex = null)
        {
            LogEnumType logType = LogEnumType.SuccessLog;
            string s = "";
            if (ex != null)
            {
                s = "Error in adding " + typeof(DomainModel).Name + $" ,exception: {ex.Message}";
                logType = LogEnumType.ErrorLog;
            }
            else
            {
                s += "Add " + typeof(DomainModel).Name;
                s += " ,ID:" + model.ID.ToString();
            }

            if (!string.IsNullOrWhiteSpace(objectDescriptor))
            {
                s += " ," + objectDescriptor;
            }
            Log(new EventId(_logBaseID + 1, "Add"), s, model?.ID.ToString(), logType);
        }
        protected void LogRetrieveSingle(PrimaryKeyType requestedID, Exception? ex = null)
        {
            LogEnumType logType = LogEnumType.SuccessLog;
            string s = "";
            if (ex != null)
            {
                s = "Error in Retrieve Single " + typeof(DomainModel).Name;
                logType = LogEnumType.ErrorLog;
            }
            else
            {
                s += "Retrieve Single " + typeof(DomainModel).Name;

            }
            s += " ,ID:" + requestedID.ToString();


            Log(new EventId(_logBaseID + 2, "RetrieveSingle"), s, requestedID.ToString(), logType);
        }
        protected void LogRetrieveSingle(string requestedDetails, Exception? ex = null)
        {
            LogEnumType logType = LogEnumType.SuccessLog;
            string s = "";
            if (ex != null)
            {
                s = "Error in Retrieve Single " + typeof(DomainModel).Name;
                logType = LogEnumType.ErrorLog;
            }
            else
            {
                s += "Retrieve Single " + typeof(DomainModel).Name;

            }
            s += " ,requestedDetails:" + requestedDetails.ToString();


            Log(new EventId(_logBaseID + 2, "RetrieveSingle"), s, null, logType);
        }
        protected void LogModify(DomainModel model, string? objectDescriptor = null, Exception? ex = null)
        {
            LogEnumType logType = LogEnumType.SuccessLog;
            string s = typeof(DomainModel).Name + " updated";
            if (ex != null)
            {
                s = "Error in updating " + typeof(DomainModel).Name;
                logType = LogEnumType.ErrorLog;
            }

            if (model != null)
            {
                s += " ,ID:" + model.ID.ToString();
            }
            if (!string.IsNullOrWhiteSpace(objectDescriptor))
            {
                s += " ," + objectDescriptor;
            }
            if (ex != null)
            {
                s += " ,exception:" + GetExceptionMessage(ex);
            }
            Log(new EventId(_logBaseID + 3, "Modify"), s, model?.ID.ToString(), logType);
        }
        protected void LogRemove(PrimaryKeyType itemID, string objectDescriptor = null, Exception ex = null)
        {
            LogEnumType logType = LogEnumType.SuccessLog;
            string s = typeof(DomainModel).Name + " removed";
            if (ex != null)
            {
                s = "Error in removing " + typeof(DomainModel).Name;
                logType = LogEnumType.ErrorLog;
            }

            s += " ,ID:" + itemID.ToString();

            if (!string.IsNullOrWhiteSpace(objectDescriptor))
            {
                s += " ," + objectDescriptor;
            }
            if (ex != null)
            {
                s += " ,exception:" + GetExceptionMessage(ex);
            }
            Log(new EventId(_logBaseID + 4, "Remove"), s, itemID.ToString(), logType);
        }

        protected void LogAndThrowNotFoundOnRemove(PrimaryKeyType requestedID)
        {
            var f = new ServiceObjectNotFoundException("Requested '" + typeof(DomainModel).Name + "' not found");
            LogRemove(requestedID, "", f);
            throw f;
        }
        protected void LogAndThrowNotFoundOnModify(DomainModel requestedModel)
        {
            var f = new ServiceObjectNotFoundException("Requested '" + typeof(DomainModel).Name + "' not found");
            LogModify(requestedModel, "", f);
            throw f;
        }


        protected void LogRetrieveMultiple(Dictionary<string, object> parameters = null, Exception ex = null)
        {
            LogEnumType logType = LogEnumType.SuccessLog;
            StringBuilder sb = new StringBuilder();
            if (ex == null)
            {
                sb.Append("Retrieve Multiple " + typeof(DomainModel).Name);
            }
            else
            {
                sb.Append("Error in Retrieve Multiple " + typeof(DomainModel).Name);
                logType = LogEnumType.ErrorLog;
            }

            if (parameters != null)
            {
                sb.Append(" ,with parameters::");
                foreach (var item in parameters)
                {
                    sb.Append(" ," + item.Key + ":" + item.Value);
                }
            }


            Log(_logBaseID + 5, sb.ToString(), null, logType);
        }

        #endregion


        #region validation

        protected abstract Task ValidateOnAddAsync(DomainModel item);
        protected abstract Task ValidateOnModifyAsync(DomainModel recievedItem, DomainModel storageItem);
 
        #endregion
    }
}
