using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;

namespace AutoGlass.Domain.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IProductRepository repo)
            : base(repo)
        {
        }
    }
}