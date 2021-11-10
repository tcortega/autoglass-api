using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoGlass.Infrastructure.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<Product> GetAll()
        {
            return _context.Set<Product>().Include(e => e.Supplier);
        }
    }
}