using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.CartItem
{
    public class CartListRequest : BaseRequest
    {
        public int Id { get; set; }
        public int ProductSKUId { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public string ModelCode { get; set; }
        public string Ram { get; set; }
        public string Storage { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }

        public string Type { get; set; }

        public double Total { get; set; }
    }
}
