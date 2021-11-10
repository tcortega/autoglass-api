using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGlass.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task Add(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
            => _context.Set<T>();

        public virtual async Task<T> Get(int id)
            => await GetAll().FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task Remove(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : Entity => _context.Attach(entity);
    }
}