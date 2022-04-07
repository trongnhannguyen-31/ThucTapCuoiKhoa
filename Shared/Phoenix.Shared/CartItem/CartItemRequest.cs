using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.CartItem
{
    public class CartItemRequest : BaseRequest
    {
        public int Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public int Quantity { get; set; }

        public int User_Id { get; set; }
    }
}
