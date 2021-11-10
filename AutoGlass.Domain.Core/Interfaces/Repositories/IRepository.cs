using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Get(int id);

        Task Add(T entity);

        Task Update(T entity);

        Task Update(IEnumerable<T> entities);

        Task Remove(T entity);

        IQueryable<T> GetAll();

        void Attach<TEntity>(TEntity entity) where TEntity : Entity;
    }
}