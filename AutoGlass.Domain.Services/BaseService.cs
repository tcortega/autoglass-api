using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Services
{
    public class BaseService<T> : IService<T> where T : Entity
    {
        protected readonly IRepository<T> _repo;

        public BaseService(IRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task Add(T entity)
        {
            await _repo.Add(entity);
        }

        public virtual async Task<T> Get(int id)
            => await _repo.Get(id);

        public virtual IEnumerable<T> GetAll()
            => _repo.GetAll().Where(s => s.IsActive);

        public async Task Remove(T entity)
        {
            entity.IsActive = false;
            await Update(entity);
        }

        public async Task Remove(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = false;
            }

            await _repo.Update(entities);
        }

        public async Task Update(T entity)
        {
            await _repo.Update(entity);
        }

        public async Task Update(IEnumerable<T> entities)
        {
            await _repo.Update(entities);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : Entity
        {
            _repo.Attach(entity);
        }
    }
}