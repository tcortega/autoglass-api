using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

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

        public override async Task Remove(int id)
        {
            var entity = await _service.Get(id);

            ValidateEntity(entity);

            var supplierProducts = _productService.GetAll()
                .Where(p => p.Supplier.Id == id);

            await _productService.Remove(supplierProducts);
            await _service.Remove(entity);
        }
    }
}