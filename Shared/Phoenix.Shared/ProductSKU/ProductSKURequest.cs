using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.ProductSKU
{
    public class ProductSKURequest : BaseRequest
    {
        public int Id { get; set; }

        public int Product_Id { get; set; }

        public double? Price { get; set; }

        public double Rating { get; set; }

        public double BuyCount { get; set; }

        public string Screen { get; set; }

        public string OperationSystem { get; set; }

        public string Processor { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public string Battery { get; set; }

        public string BackCamera { get; set; }

        public string FrontCamera { get; set; }

        public string SimSlot { get; set; }

        public string GraphicCard { get; set; }

        public string ConnectionPort { get; set; }

        public string Design { get; set; }

        public string Size { get; set; }

        public int YearOfManufacture { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
