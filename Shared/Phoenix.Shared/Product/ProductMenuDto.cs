using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Product
{
    public class ProductMenuDto
    {
        public int ProductId { get; set; }

        public int ProductType_Id { get; set; }

        public int Vendor_Id { get; set; }

        public string Name { get; set; }

        public int? Image1 { get; set; }
        public string? Image1Path { get; set; }

        public double Price { get; set; }

        public double Rating { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public int BuyCount { get; set; }

        public string ModelCode { get; set; }

        public int SKUId { get; set; }

        public string ProductName
        {
            get
            {
                if (ProductType_Id == 5)
                    return Name + " | " + Ram + "/" + Storage;
                else
                    return Name + " " + ModelCode;
            }
        }

    }
}
