using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain
{
    public interface IModel<PrimaryKey>
        where PrimaryKey : struct
    {
        /// <summary>
        /// PK Of Table
        /// </summary>
        PrimaryKey ID { get; set; }
    }
}
