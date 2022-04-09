using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Warehouse
{
    public class WarehouseOrderRequest : BaseRequest
    {
        public int Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public int Quantity { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
