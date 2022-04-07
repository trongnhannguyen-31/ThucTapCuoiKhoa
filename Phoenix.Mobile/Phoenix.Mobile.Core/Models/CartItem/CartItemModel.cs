using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Phoenix.Mobile.Core.Models.Cart
{
    public class CartItemModel
    {
        public int Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public int Quantity { get; set; }

        public int User_Id { get; set; }
    }
}
