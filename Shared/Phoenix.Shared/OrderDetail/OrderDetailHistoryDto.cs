using System;

namespace Phoenix.Shared.OrderDetail
{
	public class OrderDetailHistoryDto
	{
		public int Image { get; set; }

		public string Name { get; set; }

		public int ProductTypeId { get; set; }

		public string ModelCode { get; set; }

		public string Ram { get; set; }

		public string Storage { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public int ProductSKU_Id { get; set; }

		public int Order_Id { get; set; }

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

        public string ProductName
		{
			get
			{
				if (ProductTypeId == 5)
					return Name + " | " + Ram + "/" + Storage;
                else
                    return Name + " " + ModelCode;
            }
        }

	}
}
