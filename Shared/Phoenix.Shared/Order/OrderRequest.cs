using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Order
{
    public class OrderRequest : BaseRequest
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Address { get; set; }

        public double? Total { get; set; }

        public int Customer_Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }

        public bool CancelRequest { get; set; }
    }
}
