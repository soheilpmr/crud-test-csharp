using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read
{
    public interface IRepositoryReadAsync<DomainModelItem, PrimaryKeyType>
          where DomainModelItem : Model<PrimaryKeyType>
          where PrimaryKeyType : struct
    {
        #region single retrieve
        Task<DomainModelItem?> GetByIdAsync(PrimaryKeyType id);
        Task<DomainModelItem?> FirstOrDefaultAsync(Expression<Func<DomainModelItem, bool>> predicate);
        #endregion

        #region group retrieve
        Task<IReadOnlyList<DomainModelItem>> AllItemsAsync();
        #endregion
    }
}
