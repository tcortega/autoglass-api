using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using System;

namespace AutoGlass.Application
{
    public class ProductApplicationService : BaseApplicationService<ProductDto, Product>, IProductApplicationService
    {
        private readonly ISupplierService _supplierService;

        public ProductApplicationService(IProductService service, ISupplierService supplierService, IMapper mapper)
            : base(service, mapper)
        {
            _supplierService = supplierService;
        }

        public override void Add<TValidator>(ProductDto dto)
        {
            Validate(dto, Activator.CreateInstance<TValidator>());

            var entity = _mapper.Map<Product>(dto);

            var supplier = _supplierService.Get(entity.Supplier.Id);
            if (supplier is null || !supplier.IsActive)
                throw new Exception("Não existe nenhum fornecedor cadastrado com este id.");

            entity.Supplier = supplier;

            _service.Attach(supplier);
            _service.Update(entity);
        }

        public override void Update<TValidator>(ProductDto dto)
        {
            Validate(dto, Activator.CreateInstance<TValidator>());

            var entity = _mapper.Map<Product>(dto);

            var supplier = _supplierService.Get(entity.Supplier.Id);
            if (supplier is null || !supplier.IsActive)
                throw new Exception("Não existe nenhum fornecedor cadastrado com este id.");

            entity.Supplier = supplier;

            _service.Attach(supplier);
            _service.Update(entity);
        }
    }
}