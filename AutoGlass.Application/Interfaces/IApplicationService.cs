using AutoGlass.Application.Dtos;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGlass.Application.Interfaces
{
    public interface IApplicationService<T> where T : Dto
    {
        Task Add<TValidator>(T dto) where TValidator : AbstractValidator<T>;

        Task Update<TValidator>(T dto) where TValidator : AbstractValidator<T>;

        Task Remove(int id);

        IEnumerable<T> GetAll();

        Task<T> Get(int id);
    }
}