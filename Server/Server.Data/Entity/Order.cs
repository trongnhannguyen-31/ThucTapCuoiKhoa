using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public int StatusId { get; set; }

        public bool CancelRequest { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Address { get; set; }

        public double? Total { get; set; }

        public bool IsRated { get; set; }

        public int Customer_Id { get; set; }
        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
