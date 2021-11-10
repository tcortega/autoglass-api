using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGlass.Application
{
    public class BaseApplicationService<TDto, TEntity> : IApplicationService<TDto> where TDto : Dto
        where TEntity : Entity
    {
        protected readonly IService<TEntity> _service;
        protected readonly IMapper _mapper;

        public BaseApplicationService(IService<TEntity> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public virtual async Task Add<TValidator>(TDto dto) where TValidator : AbstractValidator<TDto>
        {
            Validate(dto, Activator.CreateInstance<TValidator>());
            var entity = _mapper.Map<TEntity>(dto);
            await _service.Add(entity);
        }

        public virtual async Task<TDto> Get(int id)
        {
            var entity = await _service.Get(id);

            ValidateEntity(entity);

            return _mapper.Map<TDto>(entity);
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var entities = _service.GetAll()
                .Where(s => s.IsActive);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task Remove(int id)
        {
            var entity = await _service.Get(id);

            ValidateEntity(entity);

            await _service.Remove(entity);
        }

        public virtual async Task Update<TValidator>(TDto dto) where TValidator : AbstractValidator<TDto>
        {
            Validate(dto, Activator.CreateInstance<TValidator>());
            var entity = _mapper.Map<TEntity>(dto);
            entity = await _service.Get(entity.Id);

            ValidateEntity(entity);

            _mapper.Map(dto, entity);

            await _service.Update(entity);
        }

        protected void Validate(TDto dto, AbstractValidator<TDto> validator)
        {
            if (dto is null)
                throw new Exception("Registros não encontrados!");

            validator.ValidateAndThrow(dto);
        }

        protected void ValidateEntity<TEnt>(TEnt entity, string msg = "Não foi encontrado nenhum registro para os dados inseridos.")
            where TEnt : Entity
        {
            if (entity is null || !entity.IsActive)
                throw new Exception(msg);
        }
    }
}