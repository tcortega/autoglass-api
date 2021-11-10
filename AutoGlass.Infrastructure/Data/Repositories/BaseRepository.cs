using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AutoGlass.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual T Get(int id)
        {
            return _context.Find<T>(id);
        }

        public virtual void Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            _context.SaveChanges();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : Entity => _context.Attach(entity);
    }
}