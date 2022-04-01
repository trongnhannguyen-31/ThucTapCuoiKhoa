using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }

        public int ProductSKU_Id { get; set; }
        [ForeignKey("ProductSKU_Id")]
        public virtual ProductSKU ProductSKU { get; set; }

        public double? Price { get; set; }

        public int? Quantity { get; set; }

        public double? Total { get; set; }
    }
}
