using GestorEvento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Core
{
    public class BaseRepository<T> where T : class
    {
        protected readonly GestorDbcontext Context;

        public BaseRepository(GestorDbcontext context)
        {
            Context = context;
        }

        public async Task<int> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();

            var property = typeof(T).GetProperty("Id");
            return (int)(property?.GetValue(entity) ?? 0);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            if (entity == null) return false;

            Context.Set<T>().Remove(entity);
            return await Context.SaveChangesAsync() > 0;
        }
    }

}
