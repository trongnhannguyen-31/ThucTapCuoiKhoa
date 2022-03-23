using Phoenix.Shared.Common;

namespace Phoenix.Shared.Warehouse
{
    public class WarehouseRequest : BaseRequest
    {
        public int Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public string ProductSKU_Name { get; set; }

        public int Quantity { get; set; }
    }
}
