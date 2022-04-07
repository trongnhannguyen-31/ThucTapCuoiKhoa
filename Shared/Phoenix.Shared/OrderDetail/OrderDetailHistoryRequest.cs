using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.OrderDetail
{
	public class OrderDetailHistoryRequest : BaseRequest
	{
		public DateTime OrderDate { get; set; }

		public string Status { get; set; }

		public DateTime? DeliveryDate { get; set; }

		public string Address { get; set; }

		public double Total { get; set; }

		public int ProductSKU_Id { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

		public string Ram { get; set; }

		public string Storage { get; set; }

		public string ProductName { get; set; }

		public int ProductTypeId { get; set; }

		public int ModelCode { get; set; }

		public int Order_Id { get; set; }

		public string Type { get; set; }
	}
}
