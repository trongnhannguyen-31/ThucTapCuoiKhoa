using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.WarehouseMenu
{
    public class WarehouseMenuRequest : BaseRequest
    {
        public int SKUId { get; set; }

        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public int ProductType_Id { get; set; }

        public string Name { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public string Model { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }
    }
}
