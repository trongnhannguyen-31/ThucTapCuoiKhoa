using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Data.Entity
{
    [Table("Warehouse")]
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        public int ProductSKU_Id { get; set; }
        [ForeignKey("ProductSKU_Id")]
        public virtual ProductSKU ProductSKU { get; set; }

        public int Quantity { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
