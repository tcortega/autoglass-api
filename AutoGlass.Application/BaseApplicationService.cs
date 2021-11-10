using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual void Add<TValidator>(TDto dto) where TValidator : AbstractValidator<TDto>
        {
            Validate(dto, Activator.CreateInstance<TValidator>());
            var entity = _mapper.Map<TEntity>(dto);
            _service.Add(entity);
        }

        public virtual TDto Get(int id)
        {
            var entity = _service.Get(id);

            if (entity is null || !entity.IsActive)
                throw new Exception("Registros não encontrados!");

            return _mapper.Map<TDto>(entity);
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var entities = _service.GetAll()
                .Where(s => s.IsActive);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual void Remove(int id)
        {
            var entity = _service.Get(id);

            if (entity is null || !entity.IsActive)
                throw new Exception("Registros não encontrados!");

            _service.Remove(entity);
        }

        public virtual void Update<TValidator>(TDto dto) where TValidator : AbstractValidator<TDto>
        {
            Validate(dto, Activator.CreateInstance<TValidator>());
            var entity = _mapper.Map<TEntity>(dto);
            entity = _service.Get(entity.Id);

            if (entity is null || !entity.IsActive)
                throw new Exception("Registros não encontrados!");

            _mapper.Map(dto, entity);

            _service.Update(entity);
        }

        protected void Validate(TDto dto, AbstractValidator<TDto> validator)
        {
            if (dto is null)
                throw new Exception("Registros não encontrados!");

            validator.ValidateAndThrow(dto);
        }
    }
}