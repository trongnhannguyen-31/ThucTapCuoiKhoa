using System;

namespace Phoenix.Server.Web.Areas.Admin.Models.ProductType
{
    public class ProductTypeModel
    {
        public int Id {  get; set; }
       
        public string Name { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}