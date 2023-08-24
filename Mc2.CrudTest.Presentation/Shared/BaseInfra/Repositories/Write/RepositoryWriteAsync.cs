using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Write
{
    public class RepositoryWriteAsync<DomainModelEntity, DomainModelItem, PrimaryKeyType> : IRepositoryWriteAsync<DomainModelItem, PrimaryKeyType>
        where DomainModelItem : Domain.Model<PrimaryKeyType>
        where DomainModelEntity : DomainModelItem
        where PrimaryKeyType : struct
    {
        protected readonly DbSet<DomainModelEntity> _entities;
        protected readonly DbContext _context;
        public RepositoryWriteAsync(DbContext dbContext)
        {
            _context = dbContext;
            _entities = dbContext.Set<DomainModelEntity>();
        }

        public virtual async Task DeleteAsync(DomainModelItem item)
        {
            if (item is DomainModelEntity)
            {
                _entities.Remove(item as DomainModelEntity);
            }
        }

        public virtual async Task<DomainModelItem> InsertAsync(DomainModelItem item)
        {
            if (item is DomainModelEntity)
            {
                return _entities.Add(item as DomainModelEntity).Entity;
            }
            else
            {
                return default(DomainModelEntity);
            }
        }

        public virtual async Task UpdateAsync(DomainModelItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
