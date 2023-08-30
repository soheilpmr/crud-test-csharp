using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;

namespace Mc2.CrudTest.Presentation.Shared.BaseServicesImplemetion
{

    public interface IStorageBusinessService<T, PrimarykeyType>
        where T : IModel<PrimarykeyType>
        where PrimarykeyType : struct
    {


        public int LogBaseID { get; }
        /// <summary>
        /// Adds an instance of the entity to the database doing all validations and exception handlings.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PrimarykeyType> AddAsync(T item);
        Task<T> RetrieveByIdAsync(PrimarykeyType ID);
        Task ModifyAsync(T item);
        Task RemoveByIdAsync(PrimarykeyType ID);
        Task<IEnumerable<T>> ItemsAsync();
    }
}
