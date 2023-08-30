using Mc2.CrudTest.Presentation.Server.BaseServiceImplemention;
using Mc2.CrudTest.Presentation.Server.BaseServiceImplemention.Exceptions;
using Mc2.CrudTest.Presentation.Server.UnitOfWork;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Write;
using Mc2.CrudTest.Presentation.Shared.Data;
using Mc2.CrudTest.Presentation.Shared.Domain;
using PhoneNumbers;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Mc2.CrudTest.Presentation.Server.Services
{
    public class CustomerService : StorageBusinessService<Customer, int>
    {
        private readonly IServerWriteUnitOfWork _serverWriteUnitOfWork;
        private readonly IServerReadUnitOfWork _serverReadUnitOfWork;
        private readonly IRepositoryWriteAsync<Customer, int> _baseRepoWrite;
        private readonly IRepositoryReadAsync<Customer, int> _baseRepoRead;
        public CustomerService(ILogger<Customer> logger, IDateTimeProviderService dateTimeProviderService,
            IServerWriteUnitOfWork serverWriteUnitOfWork, IServerReadUnitOfWork serverReadUnitOfWork) : base(logger, dateTimeProviderService, 100)
        {
            _serverWriteUnitOfWork = serverWriteUnitOfWork;
            _serverReadUnitOfWork = serverReadUnitOfWork;
            _baseRepoWrite = _serverWriteUnitOfWork.GetRepo<Customer, int>();
            _baseRepoRead = _serverReadUnitOfWork.GetRepo<Customer, int>();
        }

        public async Task<int> AddAsync(Customer item, string countryCode)
        {
            if (item == null)
            {
                var ex = new ServiceArgumentNullException("Input parameter was null:" + nameof(item));
                LogAdd(null, null, ex);
                throw ex;
            }

            if (!await LibPhoneNumberValidation(item.PhoneNumber, countryCode))
            {
                var eception = new CustomerException("PhoneNumber Is Not Valid");
                LogModify(item, null, eception);
                throw eception;
            }

            await ValidateOnAddAsync(item);

            try
            {
                var customerEntity = new CustomerEntity(item);
                var customerInserted = await _baseRepoWrite.InsertAsync(customerEntity);

                await _serverWriteUnitOfWork.CommitAsync();
                LogAdd(item, "Successfully add Customer with ,ID:" +
                  customerInserted.ID.ToString() +
                  " ,FirstName:" + item.FirstName +
                  " ,LastName:" + item.LastName);
                return customerInserted.ID;
            }
            catch (Exception ex)
            {
                LogAdd(item, objectDescriptor: $"Error Occurred in Adding a Customer With FullName : {item.FirstName + item.LastName}", ex);
                throw new ServiceStorageException("Error adding company", ex);
            }
        }

        public override async Task<List<Customer>> ItemsAsync()
        {
            return (await _serverReadUnitOfWork.customerReadRepositoryAsync.AllItemsAsync()).ToList();
        }

        public async Task ModifyAsync(Customer item, string countryCode)
        {
            if (item == null)
            {
                var exception = new ServiceArgumentNullException(typeof(Customer).Name);
                LogModify(item, null, exception);
                throw exception;
            }

            var currentItem = await _baseRepoRead.GetByIdAsync(item.ID);

            if (currentItem == null)
            {
                var noObj = new ServiceObjectNotFoundException(typeof(Customer).Name + " Not Found");
                LogModify(item, null, noObj);
                throw noObj;
            }

            if(!await LibPhoneNumberValidation(item.PhoneNumber, countryCode))
            {
                var eception = new CustomerException("PhoneNumber Is Not Valid");
                LogModify(item, null, eception);
                throw eception;
            }

            await ValidateOnModifyAsync(item, currentItem);

            currentItem.FirstName = item.FirstName;
            currentItem.LastName = item.LastName;
            currentItem.DateOfBirth = item.DateOfBirth;
            currentItem.PhoneNumber = item.PhoneNumber;
            currentItem.Email = item.Email;
            currentItem.BankAccountNumber = item.BankAccountNumber;

            try
            {
                await _serverWriteUnitOfWork.CommitAsync();
                LogModify(item, "Successfully modified Customer with ,ID:" +
                   item.ID.ToString() +
                   " ,New FirstName :" + item.FirstName +
                   " ,New LastName  :" + item.LastName);
            }
            catch (Exception ex)
            {
                LogModify(item, "Customer with ID :" + item.ID.ToString() + $"not be updated because the error message : {ex.Message}", ex);
                throw new ServiceStorageException("Error in Editing Customer", ex);
            }
        }

        public override async Task RemoveByIdAsync(int ID)
        {
            var itemToDelete = await _baseRepoRead.GetByIdAsync(ID);

            if (itemToDelete == null)
            {
                var f = new ServiceObjectNotFoundException(typeof(Customer).Name + " not found");
                LogRemove(ID, "Item With This Id Not Found", f);
                throw f;
            }


            await _baseRepoWrite.DeleteAsync(itemToDelete);
            try
            {
                await _serverWriteUnitOfWork.CommitAsync();
                LogRemove(ID, "Item Deleted Successfully", null);
            }

            catch (Exception ex)
            {
                var innerEx = new ServiceStorageException("Error deleting Customer with firstName : " + itemToDelete.FirstName, ex);
                LogRemove(ID, "Error deleting Customer with ID : " + itemToDelete.ID.ToString() + ",FullName : " + itemToDelete.FirstName + itemToDelete.LastName, ex);
                throw innerEx;
            }
        }

        public override async Task<Customer> RetrieveByIdAsync(int ID)
        {
            Customer? item;
            try
            {
                item = await _baseRepoRead.FirstOrDefaultAsync(ss => ss.ID == ID);
            }
            catch (Exception ex)
            {
                LogRetrieveSingle(ID, ex);
                throw new ServiceStorageException("Error loading Customer", ex);
            }
            if (item == null)
            {
                var f = new ServiceObjectNotFoundException(typeof(Customer).Name + " not found by id");
                LogRetrieveSingle(ID, f);
                throw f;
            }
            LogRetrieveSingle(ID);
            return item;
        }

        protected override async Task ValidateOnAddAsync(Customer item)
        {
            byte errorCount = 0;
            string errorMessage = "";
            var x = await _baseRepoRead.FirstOrDefaultAsync(ss => ss.LastName == item.LastName);
            if (x != null)
            {
                errorCount++;
                errorMessage += "FirstName IS Duplicated -";
                //throw new CustomerException($"Code {CustomerException.ExceptionCode + 2} LastName Is Repetitious");
            }

            var xx = await _baseRepoRead.FirstOrDefaultAsync(ss => ss.FirstName == item.FirstName);
            if (xx != null)
            {
                errorCount++;
                errorMessage += "LastName IS Duplicated -";
                //throw new CustomerException($"Code {CustomerException.ExceptionCode + 1} : FirstName Is Repetitious");
            }

            var xxx = await _baseRepoRead.FirstOrDefaultAsync(ss => ss.DateOfBirth == item.DateOfBirth);
            if (xxx != null)
            {
                errorCount++;
                errorMessage += "Email IS Duplicated -";
                //throw new CustomerException($"Code {CustomerException.ExceptionCode + 3}  DateOfBirth Is Repetitious");
            }

            var xxxx = await _baseRepoRead.FirstOrDefaultAsync(ss => ss.Email == item.Email);
            if (xxxx != null)
            {
                errorCount++;
                errorMessage += "DateOfBirth IS Duplicated -";
                //throw new CustomerException($"Code {CustomerException.ExceptionCode + 4}  Email Is Repetitious");
            }

            if (errorCount > 0)
            {
                if (errorMessage.EndsWith('-'))
                {
                    errorMessage = errorMessage.Substring(0, errorMessage.Length - 1);
                }
                throw new CustomerException(errorMessage);
            }
        }

        protected override async Task ValidateOnModifyAsync(Customer recievedItem, Customer storageItem)
        {
            byte errorCount = 0;
            string errorMessage = "";
            if (await _serverReadUnitOfWork.customerReadRepositoryAsync.CheckAnyCompanyExistWithFirstName(recievedItem.FirstName, recievedItem.ID))
            {
                errorCount++;
                errorMessage += "FirstName IS Duplicated -";
            }

            if (await _serverReadUnitOfWork.customerReadRepositoryAsync.CheckAnyCompanyExistWithLastName(recievedItem.LastName, recievedItem.ID))
            {
                errorCount++;
                errorMessage += "LastName IS Duplicated -";
            }

            if (await _serverReadUnitOfWork.customerReadRepositoryAsync.CheckAnyCompanyExistWithEmail(recievedItem.Email, recievedItem.ID))
            {
                errorCount++;
                errorMessage += "Email IS Duplicated -";
            }

            if (await _serverReadUnitOfWork.customerReadRepositoryAsync.CheckAnyCompanyExistWithDateOfBirth(recievedItem.DateOfBirth, recievedItem.ID))
            {
                errorCount++;
                errorMessage += "DateOfBirth IS Duplicated -";
            }

            if (errorCount > 0)
            {
                if (errorMessage.EndsWith('-'))
                {
                    errorMessage = errorMessage.Substring(0, errorMessage.Length - 1);
                }
                throw new CustomerException(errorMessage);
            }
        }

        public async Task<bool> LibPhoneNumberValidation(string telephoneNumber, string countryCode)
        {
            bool isMobile = false;
            bool isValidRegion = false;

            await Task.Run(() =>
            {
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

                PhoneNumber phoneNumber = phoneUtil.Parse(telephoneNumber, countryCode);

                isValidRegion = phoneUtil.IsValidNumberForRegion(phoneNumber, countryCode);

                string region = phoneUtil.GetRegionCodeForNumber(phoneNumber); // GB, US , PK    

                var numberType = phoneUtil.GetNumberType(phoneNumber); // Produces Mobile , FIXED_LINE    

                string phoneNumberType = numberType.ToString();

                if (!string.IsNullOrEmpty(phoneNumberType) && phoneNumberType == "MOBILE")
                {
                    isMobile = true;
                }

                var originalNumber = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164); // Produces "+923336323997"    
            });

            if (isMobile && isValidRegion)
            {
                return true;
            }
            else
                return false;
        }

        public override Task ModifyAsync(Customer item)
        {
            throw new NotImplementedException();
        }

        public override Task<int> AddAsync(Customer item)
        {
            throw new NotImplementedException();
        }
    }
}
