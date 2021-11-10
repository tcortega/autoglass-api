using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using System.Collections.Generic;

namespace AutoGlass.Domain.Services
{
    public class BaseService<T> : IService<T> where T : Entity
    {
        protected readonly IRepository<T> _repo;

        public BaseService(IRepository<T> repo)
        {
            _repo = repo;
        }

        public void Add(T entity)
        {
            _repo.Add(entity);
        }

        public virtual T Get(int id)
            => _repo.Get(id);

        public virtual IEnumerable<T> GetAll()
            => _repo.GetAll();

        public void Remove(T entity)
        {
            entity.IsActive = false;
            Update(entity);
        }

        public void Remove(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsActive = false;
            }

            _repo.Update(entities);
        }

        public void Update(T entity)
        {
            _repo.Update(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            _repo.Update(entities);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : Entity
        {
            _repo.Attach(entity);
        }
    }
}