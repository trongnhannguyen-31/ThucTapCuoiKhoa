using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Product
{
    public class ProductMenuRequest : BaseRequest
    {
        public int ProductId { get; set; }

        public int ProductType_Id { get; set; }

        public int Vendor_Id { get; set; }

        public string Name { get; set; }

        public int? Image1 { get; set; }

        public double Price { get; set; }

        public double Rating { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public int BuyCount { get; set; }

        public string ModelCode { get; set; }

        public int SKUId { get; set; }

        //public string ProductName { get { return Name + " | " +  Ram + "/" + Storage; } }
    }
}
