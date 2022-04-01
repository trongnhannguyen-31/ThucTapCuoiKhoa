using Phoenix.Shared.Common;

namespace Phoenix.Shared.OrderDetail
{
    public class OrderDetailRequest : BaseRequest
    {
		public int Id { get; set; }

		public int Order_Id { get; set; }

		public int ProductSKU_Id { get; set; }

		public double? Price { get; set; }

		public int? Quantity { get; set; }

		public double? Total { get; set; }
	}
}
