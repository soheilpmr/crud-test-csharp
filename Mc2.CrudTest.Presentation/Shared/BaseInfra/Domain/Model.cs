using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Domain
{
    public class Model<PrimaryKey> : IModel<PrimaryKey>
       where PrimaryKey : struct
    {

        [Column(Order = 0)]
        public virtual PrimaryKey ID { get; set; }
    }
}
