using System;

namespace Phoenix.Server.Web.Areas.Admin.Models.Vendor
{
    public class VendorModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Logo { get; set; }

        public string Phone { get; set; }

        public string Nation { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}