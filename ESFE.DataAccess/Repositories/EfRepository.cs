using Ardalis.Specification.EntityFrameworkCore;
using ESFE.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFE.DataAccess.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IEfRepository<T> where T : class
    {
        private readonly QuotationContext _context;
        private IDbContextTransaction? _transaction;

        public EfRepository(QuotationContext context) : base(context)        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                return;
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                return;
            }
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                return;
            }

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
