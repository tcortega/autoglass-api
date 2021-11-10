using AutoGlass.Application.Dtos;
using FluentValidation;
using System.Collections.Generic;

namespace AutoGlass.Application.Interfaces
{
    public interface IApplicationService<T> where T : Dto
    {
        void Add<TValidator>(T dto) where TValidator : AbstractValidator<T>;

        void Update<TValidator>(T dto) where TValidator : AbstractValidator<T>;

        void Remove(int id);

        IEnumerable<T> GetAll();

        T Get(int id);
    }
}