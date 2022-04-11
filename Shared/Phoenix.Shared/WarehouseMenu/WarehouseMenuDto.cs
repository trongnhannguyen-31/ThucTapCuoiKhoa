using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.WarehouseMenu
{
    public class WarehouseMenuDto
    {
        public int SKUId { get; set; }

        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public int ProductType_Id { get; set; }

        public string Name { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public string ModelCode { get; set; }

        public int Quantity { get; set; }

        public DateTime? UpdatedAt { get; set; }

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
