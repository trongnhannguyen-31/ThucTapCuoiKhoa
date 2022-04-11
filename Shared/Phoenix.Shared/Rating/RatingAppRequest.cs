﻿using Phoenix.Shared.Common;

namespace Phoenix.Shared.Rating
{
    public class RatingAppRequest : BaseRequest
    {
        public int Id { get; set; }

        public int Rate { get; set; }

        public string Comment { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public int Image1 { get; set; }

        public int Image2 { get; set; }

        public int Image3 { get; set; }

        public string Customer_Name { get; set; }

        public int ProductSKU_Id { get; set; }
    }
}
