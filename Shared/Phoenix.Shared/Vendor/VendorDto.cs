using System;

namespace Phoenix.Shared.Vendor
{
    public class VendorDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int? Logo { get; set; }

        public string Logo_Link { get; set; }
        
        public string Phone { get; set; }

        public string Nation { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
