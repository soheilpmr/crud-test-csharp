using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.Repositories.Read
{
    public abstract class RepositoryReadAsync<DomainModelEntity, DomainModelItem, PrimaryKeyType, TContext> : IRepositoryReadAsync<DomainModelItem, PrimaryKeyType>
        where DomainModelItem : Domain.Model<PrimaryKeyType>
        where DomainModelEntity : DomainModelItem
        where PrimaryKeyType : struct
        where TContext : DbContext
    {
        protected readonly DbSet<DomainModelEntity> _entities;
        private readonly DbContext _dbContext;
        public RepositoryReadAsync(TContext dbContext)
        {
            _dbContext = dbContext;    
            _entities = dbContext.Set<DomainModelEntity>();
        }

        public async Task<IReadOnlyList<DomainModelItem>> AllItemsAsync()
        {
            return await _entities.ToListAsync();
        }

        public Task<DomainModelItem?> FirstOrDefaultAsync(Expression<Func<DomainModelItem, bool>> predicate)
        {
             return _entities.FirstOrDefaultAsync(predicate);
        }

        public async Task<DomainModelItem?> GetByIdAsync(PrimaryKeyType id)
        {
            return await _entities.FindAsync(id);
        }
    }
}
