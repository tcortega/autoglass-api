using System;

namespace AutoGlass.Domain.Entities
{
    public class Product : Entity
    {
        public string Description { get; set; }

        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Supplier Supplier { get; set; }
    }
}