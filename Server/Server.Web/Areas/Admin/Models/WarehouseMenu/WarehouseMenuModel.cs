using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.WarehouseMenu
{
    public class WarehouseMenuModel
    {
        public int WarehouseId { get; set; }

        public int SKUId { get; set; }

        public int ProductId { get; set; }

        public int ProductType_Id { get; set; }

        public string Name { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}