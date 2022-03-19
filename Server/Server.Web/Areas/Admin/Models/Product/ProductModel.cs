﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.Product
{
	public class ProductModel
	{
		public int Id { get; set; }

		public int Vendor_Id { get; set; }

		public int ProductType_Id { get; set; }

		public string Name { get; set; }

		public string ModelCode { get; set; }

		public double? Rating { get; set; }

		public double Price { get; set; }

		public int? Image1 { get; set; }

		public int? Image2 { get; set; }

		public int? Image3 { get; set; }

		public int? Image4 { get; set; }

		public int? Image5 { get; set; }

		public int? ViewCount { get; set; }

		public int? CommentCount { get; set; }

		public int? BuyCount { get; set; }

		public int YearOfManufacture { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public bool Deleted { get; set; }
	}
}