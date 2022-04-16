using System;

namespace Phoenix.Shared.Order
{
    public class OrderDto
        //code test cua Man
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public int StatusId { get; set; }

        public string Cancel_Name { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Address { get; set; }

        public double? Total { get; set; }

        public int Customer_Id { get; set; }

        public string Customer_Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
