using Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read;
using Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories
{
    public interface ICompatibleRepositoryAsync<DomainModelItem, PrimaryKeyType> : IRepositoryReadAsync<DomainModelItem, PrimaryKeyType>, IRepositoryWriteAsync<DomainModelItem, PrimaryKeyType>  
         where DomainModelItem : Model<PrimaryKeyType>
         where PrimaryKeyType : struct
    {

    }
}
