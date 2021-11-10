using AutoGlass.Domain.Entities;
using System.Collections.Generic;

namespace AutoGlass.Domain.Core.Interfaces.Services
{
    public interface IService<T> where T : Entity
    {
        void Add(T entity);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Remove(T entity);

        void Remove(IEnumerable<T> entities);

        IEnumerable<T> GetAll();

        T Get(int id);

        void Attach<TEntity>(TEntity entity) where TEntity : Entity;
    }
}