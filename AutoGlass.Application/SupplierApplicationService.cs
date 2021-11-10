using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using System;
using System.Linq;

namespace AutoGlass.Application
{
    public class SupplierApplicationService : BaseApplicationService<SupplierDto, Supplier>, ISupplierApplicationService
    {
        private readonly IProductService _productService;

        public SupplierApplicationService(ISupplierService service, IProductService productService, IMapper mapper)
            : base(service, mapper)
        {
            _productService = productService;
        }

        public override void Remove(int id)
        {
            var entity = _service.Get(id);

            if (entity is null || !entity.IsActive)
                throw new Exception("Registros não encontrados!");

            var supplierProducts = _productService.GetAll()
                .Where(p => p.Supplier.Id == id);

            _productService.Remove(supplierProducts);

            _service.Remove(entity);
        }
    }
}