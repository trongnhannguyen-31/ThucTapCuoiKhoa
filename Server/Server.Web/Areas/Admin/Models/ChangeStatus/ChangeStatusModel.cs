using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.ChangeStatus
{
    public class ChangeStatusModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        // public string Status_Name { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Address { get; set; }

        public double? Total { get; set; }

        public int Customer_Id { get; set; }

        public string Customer_Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }

        public int Order_Id { get; set; }

        public int ProductSKU_Id { get; set; }

        public int Warehouse_Id { get; set; }
    }
}