using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Entities;

namespace AutoGlass.Infrastructure.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}