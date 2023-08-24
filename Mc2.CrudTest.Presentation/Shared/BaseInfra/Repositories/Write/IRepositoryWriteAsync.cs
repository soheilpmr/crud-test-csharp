using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Write
{
    public interface IRepositoryWriteAsync<DomainModelItem, PrimaryKeyType>
          where DomainModelItem : Model<PrimaryKeyType>
          where PrimaryKeyType : struct
    {
        Task<DomainModelItem> InsertAsync(DomainModelItem item);
        Task DeleteAsync(DomainModelItem item);
        Task UpdateAsync(DomainModelItem item);
    }
}
