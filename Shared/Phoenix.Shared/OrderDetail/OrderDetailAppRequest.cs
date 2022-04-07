using Phoenix.Shared.Common;

namespace Phoenix.Shared.OrderDetail
{
	public class OrderDetailAppRequest : BaseRequest
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
	}
}
