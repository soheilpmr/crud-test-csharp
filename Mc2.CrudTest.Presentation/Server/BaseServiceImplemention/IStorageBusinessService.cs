using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;

namespace Mc2.CrudTest.Presentation.Server.BaseServiceImplemention
{
    public interface IStorageBusinessService<DomainModel, PrimarykeyType>
          where DomainModel : IModel<PrimarykeyType>
          where PrimarykeyType : struct
    {
        public int LogBaseID { get; }

        /// <summary>
        /// Adds an instance of the entity to the database doing all validations and exception handlings.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PrimarykeyType> AddAsync(DomainModel item);
        Task<DomainModel> RetrieveByIdAsync(PrimarykeyType ID);
        Task ModifyAsync(DomainModel item);
        Task RemoveByIdAsync(PrimarykeyType ID);
    }
}
