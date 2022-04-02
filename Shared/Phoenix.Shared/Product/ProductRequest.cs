using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Product
{
	public class ProductRequest : BaseRequest
	{
		public int Id { get; set; }

		public int Vendor_Id { get; set; }

		public int ProductType_Id { get; set; }

		public string Name { get; set; }

		public string ModelCode { get; set; }

		public int? Image1 { get; set; }

		public int? Image2 { get; set; }

		public int? Image3 { get; set; }

		public int? Image4 { get; set; }

		public int? Image5 { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public bool Deleted { get; set; }
	}
}
