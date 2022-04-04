using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Data.Entity
{
    [Table("WarehouseMenu")]
    public class WarehouseMenu
    {
        [Key]
        public int WarehouseId { get; set; }

        public int SKUId { get; set; }

        public int ProductId { get; set; }

        public int ProductType_Id { get; set; }

        public string Name { get; set; }

        public string Ram { get; set; }

        public string Storage { get; set; }

        public string ModelCode { get; set; }

        public int Quantity { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
