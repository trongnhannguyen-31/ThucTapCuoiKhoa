using System;

namespace Phoenix.Shared.CartItem
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public int Quantity { get; set; }

        public int User_Id { get; set; }
    }
}
