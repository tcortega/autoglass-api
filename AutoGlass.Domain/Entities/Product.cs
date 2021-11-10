using System;

namespace AutoGlass.Domain.Entities
{
    public class Product : Entity
    {
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime ManufacturedAt { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Supplier Supplier { get; set; }
    }
}
