using Phoenix.Shared.Common;

namespace Phoenix.Shared.OrderDetail
{
    public class OrderDetailRequest : BaseRequest
    {
		public int Id { get; set; }

		public int Order_Id { get; set; }

		public int Product_Id { get; set; }

		public float Price { get; set; }

		public int Quantity { get; set; }

		public float Total { get; set; }
	}
}
