using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Server.Data.Entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int Vendor_Id { get; set; }
        [ForeignKey("Vendor_Id")]
        public virtual Vendor Vendor { get; set; }

        public int ProductType_Id { get; set; }
        [ForeignKey("ProductType_Id")]
        public virtual ProductType ProductType { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public double? Rating { get; set; }

        public int? Image1 { get; set; }
        [ForeignKey("Image1")]
        public virtual ImageRecord ImageRecord1 { get; set; }

        public int? Image2 { get; set; }
        [ForeignKey("Image2")]
        public virtual ImageRecord ImageRecord2 { get; set; }

        public int? Image3 { get; set; }
        [ForeignKey("Image3")]
        public virtual ImageRecord ImageRecord3 { get; set; }

        public int? Image4 { get; set; }
        [ForeignKey("Image4")]
        public virtual ImageRecord ImageRecord4 { get; set; }

        public int? Image5 { get; set; }
        [ForeignKey("Image5")]
        public virtual ImageRecord ImageRecord5 { get; set; }

        public int? ViewCount { get; set; }

        public int? CommentCount { get; set; }

        public int? BuyCount { get; set; }

        public int? YearOfManufacture { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}
