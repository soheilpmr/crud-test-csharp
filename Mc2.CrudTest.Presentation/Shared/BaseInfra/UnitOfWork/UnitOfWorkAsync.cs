using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared.BaseInfra.UnitOfWork
{
    internal class UnitOfWorkAsync<Context> : IUnitOfWorkAsync
        where Context : DbContext
    {
        private readonly Context _context;
        private bool disposed = false;
        public UnitOfWorkAsync(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
