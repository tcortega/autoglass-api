using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;

namespace AutoGlass.Domain.Services
{
    public class SupplierService : BaseService<Supplier>, ISupplierService
    {
        public SupplierService(ISupplierRepository repo)
            : base(repo)
        {
        }
    }
}