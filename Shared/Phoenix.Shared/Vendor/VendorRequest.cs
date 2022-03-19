using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Vendor
{
    public class VendorRequest : BaseRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int? Logo { get; set; }
        
        public string Phone { get; set; }

        public string Nation { get; set; }

        public bool Deleted { get; set; }

        public int ProductType_Id { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
