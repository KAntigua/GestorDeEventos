using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestorDbcontext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(GestorDbcontext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _transaction;
        }
    }
}
