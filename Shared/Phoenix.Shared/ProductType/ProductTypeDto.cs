using System;

namespace Phoenix.Shared.ProductType
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
