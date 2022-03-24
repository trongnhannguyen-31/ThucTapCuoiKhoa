using Falcon.Web.Core.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int Rate { get; set; }

        public string Comment { get; set; }

        public int? Image1 { get; set; }
        [ForeignKey("Image1")]
        public virtual ImageRecord ImageRecord1 { get; set; }

        public int? Image2 { get; set; }
        [ForeignKey("Image2")]
        public virtual ImageRecord ImageRecord2 { get; set; }

        public int? Image3 { get; set; }
        [ForeignKey("Image3")]
        public virtual ImageRecord ImageRecord3 { get; set; }

        public int Customer_Id { get; set; }
        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public virtual Product Product { get; set; }

        public bool Deleted { get; set; }
    }
}
