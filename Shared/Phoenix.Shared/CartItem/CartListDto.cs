using System;

namespace Phoenix.Shared.CartItem
{
    public class CartListDto
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
        public string Type
        {
            get
            {
                if (ProductTypeId == 5)
                    return "Loại: " + Ram + "/" + Storage;
                else
                    return "Model: " + ModelCode;
            }
        }

        public double Total
        {
            get
            {
                return Quantity * Price;
            }
        }
    }
}
