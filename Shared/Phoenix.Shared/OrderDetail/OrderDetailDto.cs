namespace Phoenix.Shared.OrderDetail
{
	public class OrderDetailDto
	{
		public int Id { get; set; }

		public int Order_Id { get; set; }

		public int Product_Id { get; set; }

		public string Product_Name { get; set; }

		public float Price { get; set; }

		public int Quantity { get; set; }

		public float Total { get; set; }
	}
}
