using System;

namespace AutoGlass.Application.Dtos
{
    public class ProductDto : Dto
    {
        public string Description { get; set; }

        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public SupplierDto Supplier { get; set; }
    }
}