﻿using AutoGlass.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AutoGlass.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Remove(T entity);

        IQueryable<T> GetAll();

        T Get(int id);

        void Attach<TEntity>(TEntity entity) where TEntity : Entity;
    }
}