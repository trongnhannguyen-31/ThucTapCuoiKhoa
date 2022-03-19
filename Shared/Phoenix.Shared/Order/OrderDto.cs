﻿using System;

namespace Phoenix.Shared.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int Status { get; set; }

        public string Status_Name { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Address { get; set; }

        public float Total { get; set; }

        public int Customer_Id { get; set; }

        public string Customer_Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
