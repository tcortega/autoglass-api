using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using System;
using System.Threading.Tasks;

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

        public override async Task Add<TValidator>(ProductDto dto)
        {
            Validate(dto, Activator.CreateInstance<TValidator>());

            var entity = _mapper.Map<Product>(dto);
            var supplier = await _supplierService.Get(entity.Supplier.Id);

            ValidateEntity(supplier, "Não existe nenhum fornecedor cadastrado com este id.");

            entity.Supplier = supplier;

            _service.Attach(supplier);
            await _service.Update(entity);
        }

        public override async Task Update<TValidator>(ProductDto dto)
        {
            Validate(dto, Activator.CreateInstance<TValidator>());

            var entity = _mapper.Map<Product>(dto);
            var supplier = await _supplierService.Get(entity.Supplier.Id);

            entity = await _service.Get(entity.Id);

            ValidateEntity(entity, "Não existe nenhum produto cadastrado com este id.");
            ValidateEntity(supplier, "Não existe nenhum fornecedor cadastrado com este id que seja dono desse produto.");

            _mapper.Map(dto, entity);
            entity.Supplier = supplier;

            _service.Attach(supplier);
            await _service.Update(entity);
        }
    }
}