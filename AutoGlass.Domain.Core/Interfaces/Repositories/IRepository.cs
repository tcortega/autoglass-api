using AutoGlass.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
    }
}
