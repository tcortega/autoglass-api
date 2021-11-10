using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Core.Interfaces.Services
{
    public interface IService<T> where T : Entity
    {
        Task Add(T entity);

        Task Update(T entity);

        Task Update(IEnumerable<T> entities);

        Task Remove(T entity);

        Task Remove(IEnumerable<T> entities);

        IEnumerable<T> GetAll();

        Task<T> Get(int id);

        void Attach<TEntity>(TEntity entity) where TEntity : Entity;
    }
}